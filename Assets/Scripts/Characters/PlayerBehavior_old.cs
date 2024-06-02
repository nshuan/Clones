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
public class PlayerBehaviour : CharacterBehavior
{
    [HideInInspector] public Inventory inventory;
    private PlayerCharacterSO _charData;

    [SerializeField] private bool isImmortal = false;

    private int _currentHealth;
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
    
    private void Start()
    {
        SetupCharacter(PlayerManager.Instance.CurrentCharacter);
        SetupGun();
        _currentHealth = _charData.MaxHealth;
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
    
    void FixedUpdate()
    {
        if (dashCdCounter < dashCooldown)
        {
            dashCdCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f);
        }

        switch (charState)
        {
            case State.stand:
                // Aim(tempAimTargetPos);

                if (standCounter < standDuration) 
                    standCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f);
                else
                {
                    standCounter = 0f;
                    charState = State.move;
                }

                break;
            
            case State.move:
                // Aim(tempAimTargetPos);
                MoveWithRigidbody(tempMoveDirection, _charData.Speed);
                break;
            
            case State.dash:
                // Aim(tempAimTargetPos);

                if (Vector2.Distance(transform.position, slipTarget) > 0.1f && slipCounter < slipDuration)
                {
                    var speed = 2;
                    slipCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f);
                    // transform.position = Vector2.SmoothDamp(transform.position, slipTarget, ref slipVel, 0.2f * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f));
                    transform.position = Vector2.MoveTowards(transform.position, 
                                            slipTarget, 0.02f * speed * dashSpeedScale * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f));
                    break;
                }

                damageImmune = false;
                charState = State.move;
                break;

            case State.freeze:
                break;
            
            case State.bounce:
                if (slipCounter < slipDuration)
                {
                    slipCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f);
                    transform.position = Vector2.SmoothDamp(transform.position, slipTarget, ref slipVel, 0.2f * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f));
                    break;
                }
                
                charState = State.move;    
                break;
        }
    }
    
    private void SetupCharacter(PlayerCharacterSO charInfo)
    {
        _charData = charInfo;
        
        // Setup visual
        spriteRenderer.sprite = charInfo.AvatarSprite;
        spriteRenderer.color = charInfo.Color;
    }

    private void SetupGun()
    {
        gun = GunManager.Instance.GetGun(_charData.DefaultGun);
        inventory = new Inventory(gun);
        gunBullet = gun.GetBullet();
        bulletColor = spriteRenderer.color;
        UIManager.Instance.UpdateGunName(gun.GetName());
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
                // HitEnemy(hit.transform);
                // Bounce(transform.position - hit.transform.position, 3f, 0.5f);
            }
            else
            {
                // Vector3 inVector = (transform.position - (Vector3)hit.point).normalized;
                // Bounce(hit.transform.up - inVector * (0.5f / Mathf.Cos(Vector2.Angle(hit.transform.up, inVector))), 3f, 0.5f);
                // Bounce(hit.transform.up * 2 - (transform.position - (Vector3)hit.point).normalized, 4f, 0.5f);
            }
        }
    }

    public override void HitEnemy(Transform enemy)
    {
        // if dashing then enemy died
        if (charState == State.dash)
        {
            enemy.GetComponent<CharacterBehavior>().Damaged(10 + _charData.Damage + PlayerData.Level);
        }
        
        Damaged(enemy.GetComponent<CharacterBehavior>().HitPlayer());        
    }

    public override int HitPlayer()
    {
        return _charData.Damage;
    }

    public void UpdateGun()
    {
        gun = GunManager.Instance.GetGun(inventory.GetCurrentGunId());
        gunBullet = gun.GetBullet();
        UIManager.Instance.UpdateGunName(gun.GetName());
    }

    public override void Damaged(int value)
    {
        if (isImmortal) return;
        if (damageImmune) return;
        
        _currentHealth -= value;
        UIManager.Instance.UpdateHealthBar(_currentHealth, _charData.MaxHealth);

        if (_currentHealth < 0)
            Die();
    }
    
    public void Heal(int value)
    {
        _currentHealth = Mathf.Min(_currentHealth + value, _charData.MaxHealth);
        UIManager.Instance.UpdateHealthBar(_currentHealth, _charData.MaxHealth);
    }

    public override void Die()
    {
        GameManager.Instance.GameOver();
        Destroy(gameObject);
    }
    #endregion
    
    /// <summary>
    /// Calculate new position of character, using Rigidbody.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    protected void MoveWithRigidbody(Vector2 direction, float speed)
    {
        // If the character is moving diagonally, speed is multiply by 1.35f
        float speedScale = 1f;
        if (Mathf.Abs(direction.x) > 0.5f && Mathf.Abs(direction.y) > 0.5f)
        {
            speedScale = 1.35f;
        }

        rb2d.velocity = direction.normalized * (speed * speedScale * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f));
    }
}
