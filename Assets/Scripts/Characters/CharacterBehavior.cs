using System.Collections;
using System.Collections.Generic;
using Core.ObjectPooling;
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
    protected float timeScaleResistant = 0f;
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
    protected Vector2 tempAimTargetPos;

    // If character does not use gun, gun is null.
    // Character keeps firing as long as isFiring = true.
    // Every gun has cooldown time.
    protected Gun gun;
    protected BulletBehavior gunBullet;
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

    protected virtual void Awake()
    {
        bulletOffset = 1.5f;

        charState = State.stand;
    }
    

    #region Movement
   
     
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
            // GameObject b = Instantiate(gunBullet, transform.position + transform.up * bulletOffset, transform.rotation);
            var bullet = PoolManager.Instance.Get(gunBullet);
            bullet.transform.position = transform.position + transform.up * bulletOffset;
            bullet.transform.rotation = transform.rotation;
            bullet.transform.localScale *= gun.GetBulletScale();

            Vector2 originalDir = fireDirection.normalized;
            Vector2 deflection = Vector2.Perpendicular(originalDir) * Random.Range(-gun.GetSpread(), gun.GetSpread());
            // bullet.SetBulletStats(damage + gun.GetBaseDamage(), gun.GetBulletSpeed(), gun.GetBulletLifeLength(), originalDir + deflection, bulletColor - new Color(0f, 0f, 0f, 0.5f), LayerMask.LayerToName(gameObject.layer) + "Bullet");
            
            bullet.SetBulletStats(0 + gun.GetBaseDamage(), gun.GetBulletSpeed(), gun.GetBulletLifeLength(), originalDir + deflection, bulletColor - new Color(0f, 0f, 0f, 0.5f), LayerMask.LayerToName(gameObject.layer) + "Bullet");
        }
        
        
    }
    #endregion
}
