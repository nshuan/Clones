using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    move,
    stand,
    dash,
    freeze,
    bounce
}

/// <summary>
/// Abstract class for every characters in the game, including player and enemies.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class CharacterBehavior : MonoBehaviour
{
    #region STATS
    protected int maxHealth = 100;
    protected int health;
    protected int damage = 2;
    protected float speed = 10;
    protected float timeScaleResistant = 0f;
    #endregion

    #region Avatar
    protected Sprite sprite;
    #endregion

    #region Serializable
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Collider2D cld;
    [SerializeField] protected Animator animator;

    // Using rigidbody
    // useRigidbody = true -> character moves with rigidbody
    // useRigidbody = false -> character moves with transform
    [SerializeField] protected bool useRigidbody;
    [SerializeField] protected Rigidbody2D rb2d;
    #endregion

    #region Temporary variables
    protected State charState = new State();
    protected Vector2 tempMoveDirection;
    protected Vector3 tempMoveTargetPos;
    protected Vector3 tempAimTargetPos;

    // If character does not use gun, gun is null.
    // Character keeps firing as long as isFiring = true.
    // Every gun has cooldown time.
    protected Gun gun;
    protected GameObject gunBullet;
    protected bool isFiring = false;
    protected float gunCooldown;
    protected float gunCdCounter = 0f;
    protected float bulletOffset;
    protected Vector2 fireDirection;
    protected Color bulletColor;

    protected bool damageImmune = false;

    protected float standDuration = 1f;
    protected float standCounter = 0f;

    protected bool freeze = false;

    // These variables is used for both bounce and dash technique.
    // slipTarget is the predicted position of character after dashed or bounced.
    // Dashing has cooldown, bouncing does not.
    protected Vector2 slipTarget;
    protected Vector2 slipVel;
    protected float slipDuration = 0f;
    protected float slipCounter = 0f;
    protected float dashCooldown = 3f;
    protected float dashCdCounter = 0f;
    protected float dashSpeedScale;
    #endregion

    #region Abstract methods
    public abstract void HitEnemy(Transform enemy);
    public abstract int HitPlayer();    // return damage
    public abstract void Damaged(int value);
    public abstract void Die();
    #endregion

    void Awake()
    {
        health = maxHealth;
        bulletOffset = 1.5f;

        charState = State.stand;
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
                Aim(tempAimTargetPos);

                if (standCounter < standDuration) 
                    standCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f);
                else
                {
                    standCounter = 0f;
                    charState = State.move;
                }

                break;
            
            case State.move:
                Aim(tempAimTargetPos);
                
                if (useRigidbody)
                {
                    MoveWithRigidbody(tempMoveDirection);
                }
                else
                {
                    Move(tempMoveTargetPos);
                }
                break;
            
            case State.dash:
                Aim(tempAimTargetPos);

                if (Vector2.Distance(transform.position, slipTarget) > 0.1f && slipCounter < slipDuration)
                {
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

    #region Get things
    public Collider2D GetCollider2D()
    {
        return this.cld;
    }
    #endregion

    #region Movement
    /// <summary>
    /// Calculate the rotation of character. Character will face targetPos.
    /// </summary>
    /// <param name="targetPos"></param>
    protected void Aim(Vector3 targetPos)
    {
        Vector2 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    /// <summary>
    /// Calculate new position of character, using Vector2.MoveTowardS(...).
    /// </summary>
    /// <param name="targetPos"></param>
    protected void Move(Vector3 targetPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, 
                                            targetPos, 0.02f * speed * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f));
    }

    /// <summary>
    /// Calculate new position of character, using Rigidbody.
    /// </summary>
    /// <param name="direction"></param>
    protected void MoveWithRigidbody(Vector2 direction)
    {
        // If the character is moving diagonally, speed is multiply by 1.35f
        float speedScale = 1f;
        if (Mathf.Abs(direction.x) > 0.5f && Mathf.Abs(direction.y) > 0.5f)
        {
            speedScale = 1.35f;
        }

        rb2d.velocity = direction.normalized * speed * speedScale * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f);
    }

    /// <summary>
    /// Character can not move in the period of duration.
    /// </summary>
    /// <param name="duration"></param>
    protected void Stand(float duration)
    {
        standDuration = duration;
        standCounter = 0f;

        charState = State.stand;
    }
    
    /// <summary>
    /// Character can not do anything in freezing.
    /// </summary>
    public void Freeze()
    {
        freeze = true;

        if (rb2d != null)
            rb2d.velocity = Vector2.zero;

        charState = State.freeze;
    }

    /// <summary>
    /// Finish freezing.
    /// </summary>
    public void Thaw()
    {
        freeze = false;

        charState = State.move;
    }

    /// <summary>
    /// Bounce back.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="length"></param>
    /// <param name="duration"></param>
    protected void Bounce(Vector2 direction, float length, float duration)
    {
        slipTarget = (Vector2) transform.position + direction.normalized * length;
        slipDuration = duration;
        slipCounter = 0f;

        damageImmune = false;
        charState = State.bounce;
    }

    protected void Dash(Vector2 direction, float length, float dashSp)
    {
        if (dashCdCounter < dashCooldown) return;

        slipTarget = (Vector2) transform.position + direction.normalized * length;
        slipDuration = 1.5f;
        slipCounter = 0f;
        dashCdCounter = 0f;
        dashSpeedScale = dashSp;

        damageImmune = true;
        charState = State.dash;
    }
    #endregion

    #region Battle
    /// <summary>
    /// Fire.
    /// Instantiate a bullet from buller prefab and set its original position and direction.
    /// Also set bullet stats such as damage, speed, life length, color, obstacle layers.
    /// * bullet damage = character damage + bullet base damage.
    /// </summary>
    protected void Fire()
    {
        if (gun == null) return;
        if (gunBullet == null) return;
        if (freeze) return;

        for (int i = 0; i < gun.GetBulletNum(); i++)
        {
            GameObject b = Instantiate(gunBullet, transform.position + transform.up * bulletOffset, transform.rotation);
            BulletBehavior bScript = b.GetComponent<BulletBehavior>();
            b.transform.localScale *= gun.GetBulletScale();

            Vector2 originalDir = fireDirection.normalized;
            Vector2 deflection = Vector2.Perpendicular(originalDir) * Random.Range(-gun.GetSpread(), gun.GetSpread());
            bScript.SetBulletStats(damage + gun.GetBaseDamage(), gun.GetBulletSpeed(), gun.GetBulletLifeLength(), originalDir + deflection, bulletColor - new Color(0f, 0f, 0f, 0.5f), LayerMask.LayerToName(gameObject.layer) + "Bullet");
        }
    }
    #endregion
}
