using Managers;
using System.Collections;
using System.Collections.Generic;
using Scripts.PlayerSettings;
using UnityEngine;

/// <summary>
/// Inherited from class CharacterBehavior.
/// Can hit other CharacterBehavior objects.
/// Can switch gun and be healed.
/// </summary>/
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Inventory))]
public class PlayerBehavior : CharacterBehavior
{
    [HideInInspector] public Inventory inventory;
    private PlayerCharacterSO _charData;

    protected override void Awake()
    {
        base.Awake();

        PlayerInputActionManager.onMove += OnMove;
        PlayerInputActionManager.onAim += OnAim;
        PlayerInputActionManager.onDash += OnDash;
        PlayerInputActionManager.onFire += OnFire;
        PlayerInputActionManager.onSwitchGun += OnSwitchGun;
        PlayerInputActionManager.onQuitGunReplacement += OnQuitGunReplacement;
    }
    
    void Start()
    {
        SetupCharacter(PlayerManager.Instance.CurrentCharacter);
        
        gun = GunManager.Instance.GetGun(_charData.DefaultGun);
        inventory = new Inventory(gun);
        gunBullet = gun.GetBullet();
        bulletColor = spriteRenderer.color;
        UIManager.Instance.UpdateGunName(gun.GetName());

        timeScaleResistant = 1f;
    }

    void Update()
    {
        CheckHit();

        if (gun == null) return;
        gunCooldown = 1 / 1.6f / gun.GetFireRate();
        if (gunCdCounter < gunCooldown)
        {
            gunCdCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f);
        }
        else
        {
            if (isFiring)
            {
                fireDirection = tempAimTargetPos - (Vector2)transform.position;
                Fire();
                SoundManager.Instance.PlayFireSound();
                gunCdCounter = 0f;

                if (gun.IsAutomatic() == false)
                    isFiring = false;
            }
        }
    }
    
    public void SetupCharacter(PlayerCharacterSO charInfo)
    {
        _charData = charInfo;
        
        // Setup stats
        maxHealth = charInfo.MaxHealth;
        damage = charInfo.Damage;
        speed = charInfo.Speed;
        
        // Setup visual
        spriteRenderer.sprite = charInfo.AvatarSprite;
        spriteRenderer.color = charInfo.Color;
    }

    #region Player input
    protected void OnMove(Vector2 direction)
    {
        tempMoveDirection = direction;
    }
    
    protected void OnAim(Vector2 target)
    {
        tempAimTargetPos = target;
    }

    protected void OnDash()
    {
        Dash(tempAimTargetPos - (Vector2) transform.position, 8f, 3.6f);
    }

    protected void OnFire(bool firing)
    {
        this.isFiring = firing;
    }
    
    protected void OnSwitchGun()
    {
        // When choosing gun slot to replace new gun, player can press Q to quit choosing if they do not want to change guns
        if (UIManager.Instance.replacingGun)
        {
            UIManager.Instance.replaceGunBoard.EndChoosing();
            return;
        }

        inventory.SwitchNextGun();
        UpdateGun();
    }

    protected void OnQuitGunReplacement()
    {
        if (UIManager.Instance.replacingGun)
        {
            UIManager.Instance.replaceGunBoard.EndChoosing();
            return;
        }
    }
    #endregion

    #region Battle
    private void CheckHit()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1f, Vector2.up, 0.001f, LayerMask.GetMask("Enemy", "Wall"));

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Enemy")
            {
                HitEnemy(hit.transform);
                Bounce(transform.position - hit.transform.position, 3f, 0.5f);
            }
            else
            {
                // Vector3 inVector = (transform.position - (Vector3)hit.point).normalized;
                // Bounce(hit.transform.up - inVector * (0.5f / Mathf.Cos(Vector2.Angle(hit.transform.up, inVector))), 3f, 0.5f);
                Bounce(hit.transform.up * 2 - (transform.position - (Vector3)hit.point).normalized, 4f, 0.5f);
            }
        }
    }

    public override void HitEnemy(Transform enemy)
    {
        // if dashing then enemy died
        if (charState == State.dash)
        {
            enemy.GetComponent<CharacterBehavior>().Damaged(10 + damage + PlayerData.Level);
        }
        
        Damaged(enemy.GetComponent<CharacterBehavior>().HitPlayer());        
    }

    public override int HitPlayer()
    {
        return damage;
    }

    public void UpdateGun()
    {
        gun = GunManager.Instance.GetGun(inventory.GetCurrentGunId());
        gunBullet = gun.GetBullet();
        UIManager.Instance.UpdateGunName(gun.GetName());
    }

    public override void Damaged(int value)
    {
        if (damageImmune) return;
        
        health -= value;
        UIManager.Instance.UpdateHealthBar(health, maxHealth);

        if (health < 0)
            Die();
    }
    
    public void Heal(int value)
    {
        health = Mathf.Min(health + value, maxHealth);
        UIManager.Instance.UpdateHealthBar(health, maxHealth);
    }

    public override void Die()
    {
        GameManager.Instance.GameOver();
        Destroy(gameObject);
    }
    #endregion
}
