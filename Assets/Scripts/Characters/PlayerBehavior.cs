using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Inherited from class CharacterBehavior.
/// Include input callback functions for user input.
/// Can hit other CharacterBehavior objects.
/// Can switch gun and be healed.
/// </summary>/
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Inventory))]
public class PlayerBehavior : CharacterBehavior
{
    [Header("Player Fields")]

    #region User input
    private Vector2 movementInput;
    private Vector2 mousePos;
    #endregion

    [HideInInspector] public Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();

        gun = GunCollection.GetGun(inventory.GetCurrentGunId());
        gunBullet = GunCollection.bulletCollection.GetBullet(gun.GetBulletType());
        bulletColor = spriteRenderer.color;
        UIManager.Instance.UpdateGunName(gun.GetName());

        timeScaleResistant = 1f;
    }

    void Update()
    {
        tempMoveDirection = movementInput;
        tempAimTargetPos = mousePos;

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
                fireDirection = tempAimTargetPos - transform.position;
                Fire();
                SoundManager.Instance.PlayFireSound();
                gunCdCounter = 0f;

                if (gun.IsAutomatic() == false)
                    isFiring = false;
            }
        }
    }

    #region Player input
    /// <summary>
    /// Get user's movement input
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Get user's mouse position
    /// </summary>
    /// <param name="context"></param>
    public void OnAim(InputAction.CallbackContext context)
    {
        mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Dash(mousePos - (Vector2) transform.position, 8f, 3.6f);
        }
    }

    /// <summary>
    /// Called when fire button is pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isFiring = true;
        }
        else if (context.canceled)
        {
            isFiring = false;
        }
    }

    /// <summary>
    /// Called when switch gun button is pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnSwitchGun(InputAction.CallbackContext context)
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
        gun = GunCollection.GetGun(inventory.GetCurrentGunId());
        gunBullet = GunCollection.bulletCollection.GetBullet(gun.GetBulletType());
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
