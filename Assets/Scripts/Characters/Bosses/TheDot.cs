using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDot : CharacterBehavior
{
    private Transform target;

    #region Serializable
    protected int soulFragment;
    [SerializeField] private float attackRange = 12f;
    #endregion

    #region Guns
    private Gun cherry8 = new Gun("cherry8", -1, 10, 8, 2, 0f, 2f, 0.25f, 1, 0f);
    private Gun cherry16 = new Gun("cherry16", -1, 10, 16, 3, 0f, 2f, 0f, 1, 0f);
    #endregion

    #region Temporaty variables
    private int phase = 1;

    private float attackCooldown = 3f;
    private float attackCdCounter = 0f;
    #endregion

    void Start()
    {
        target = GameManager.Instance.player;

        gun = cherry8;
        gunBullet = GunCollection.bulletCollection.GetBullet(gun.GetBulletType());
        gunCooldown = 1 / 1.6f / gun.GetFireRate();

        maxHealth = 1000;
        health = maxHealth;
        speed = 6;
        bulletOffset = 0f;
        soulFragment = 8;
    }

    void Update()
    {
        if (target == null) return; // Maybe a wandering system is more appropriate

        tempAimTargetPos = target.position;
        tempMoveTargetPos = target.position;

        CheckHitPlayer();

        if (attackCdCounter < attackCooldown)
        {
            attackCdCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f);
        }
        else
        {
            if (Vector2.Distance(transform.position, target.position) < attackRange)
            {
                if (Random.Range(0f, 1f) < 0.4f)
                {
                    Dash(target.position - transform.position, (target.position - transform.position).magnitude * 1.5f, 3f);
                    isFiring = false;
                    attackCdCounter = 0f;
                }
                else 
                {
                    Stand(3f);
                    fireDirection = tempAimTargetPos - transform.position;
                    isFiring = true;
                    attackCdCounter = 0f;
                } 
            }
        }

        if (gunCdCounter < gunCooldown)
        {
            gunCdCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f);
        }
        else
        {
            if (isFiring)
            {
                Fire();
                gunCdCounter = 0f;
                isFiring = false;
            }
        }
    }

    public TheDot SetupStats(string name, int maxHealth, int soul, int damage, float speed, float attackRange, float sizeScale)
    {
        this.name = name;
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.soulFragment = soul;
        this.damage = damage;
        this.speed = speed;
        this.attackRange = attackRange;
        transform.localScale *= sizeScale;

        return this;
    }

    private void CheckHitPlayer()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1f, Vector2.up, 0.001f, LayerMask.GetMask("Player"));

        if (hit.collider != null)
        {
            HitPlayer();
        }
    }

    public override void HitEnemy(Transform enemy)
    {
        enemy.GetComponent<EnemyBehavior>().HitPlayer();
    }

    public override int HitPlayer()
    {
        Stand(1f);
        Bounce(transform.position - target.position, 3f / transform.localScale.x, 0.5f);

        return damage;
    }

    public override void Damaged(int value)
    {
        if (damageImmune) return;
        
        health -= value;
        UIManager.Instance.UpdateBossHealthBar(health, maxHealth);
        if (health < maxHealth / 2)
        {
            if (phase == 1)
            {
                phase = 2;
                gun = cherry16;
                gunBullet = GunCollection.bulletCollection.GetBullet(gun.GetBulletType());
            }
        }

        if (health < 0)
        {
            Die();
            return;
        }

    }

    public override void Die()
    {
        EnemyManager.Instance.BossDied(soulFragment);

        Destroy(gameObject);
    }
}
