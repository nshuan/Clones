using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehavior : CharacterBehavior
{
    [Header("Enemy Fields")]

    protected int soulFragment;
    protected int coin;
    private int id;

    #region Serializable
    [SerializeField] private float attackRange = 12f;
    [SerializeField] private ParticleSystem dieFx;
    [SerializeField] private ParticleSystem coinFx;
    [SerializeField] private ParticleSystem soulFx;
    #endregion

    private Transform target;
    private int health;

    private List<ItemData> itemHolding = new List<ItemData>();

    void Start()
    {
        target = GameManager.Instance.player;
        
        bulletColor = Color.red;
    }

    void Update()
    {
        if (target == null) return;
        
        tempAimTargetPos = target.position;
        tempMoveTargetPos = target.position;

        // Second, check if the target is in attack range. If yes, set isFiring = true, else false.
        if (gun == null) return;
        gunCooldown = 1 / 1.6f / gun.GetFireRate();
        if (Vector2.Distance(transform.position, target.position) < attackRange)
            isFiring = true;       
        else
            isFiring = false;

        // Then check if the gun cooldown is finished.
        // When the enemy fires, it can not move.
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
                Stand(Random.Range(gunCooldown / 4, gunCooldown * 3 / 4));
                gunCdCounter = 0f;
            }
        }
    }

    public EnemyBehavior SetupStats(string name, int id, int maxHealth, int soul, int coin, int damage, float speed, float attackRange, float sizeScale, Color color, Gun gun)
    {
        this.name = name;
        this.id = id;
        // this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.soulFragment = soul;
        this.coin = coin;
        // this.damage = damage;
        // this.speed = speed;
        this.attackRange = attackRange;
        transform.localScale *= sizeScale;
        spriteRenderer.color = color;

        ParticleSystem.MainModule dieFxM = dieFx.main;
        dieFxM.startColor = new ParticleSystem.MinMaxGradient(color - new Color(0f, 0f, 0f, 0.75f), color - new Color(0f, 0f, 0f, 0.25f));
        ParticleSystem.TriggerModule coinFxTr = coinFx.trigger;
        // coinFxTr.AddCollider(GameManager.Instance.playerScript.GetCollider2D());
        ParticleSystem.TriggerModule soulFxTr = soulFx.trigger;
        // soulFxTr.AddCollider(GameManager.Instance.playerScript.GetCollider2D());

        this.gun = gun;

        if (gun != null)
        {
            gunBullet = gun.GetBullet();
            gunCooldown = 1 / gun.GetFireRate();
        }

        return this;
    }   

    public EnemyBehavior SetupItems(float healthRate, float gunRate)
    {
        if (Random.Range(0f, 1f) < healthRate)
        {
            itemHolding.Add(new ItemData("Health", 0, Random.Range(10, 30)));
        }
        if (Random.Range(0f, 1f) < gunRate)
        {
            Gun newGun = GunManager.Instance.GetRandomGun();
            itemHolding.Add(new ItemData(newGun.GetName(), 1, newGun.GetId()));
        }

        return this;
    }

    #region Battle
    public override void HitEnemy(Transform enemy)
    {
        // Do nothing, enemy can not hit enemy
    }

    public override int HitPlayer()
    {
        Stand(1f);
        Bounce(transform.position - target.position, 3f / transform.localScale.x, 0.5f);

        return 0;
        // return damage;
    }

    public void DropItems()
    {
        if (EnemyManager.Instance.itemCollection == null) return;

        foreach (ItemData item in itemHolding)
        {
            GameObject newItem = Instantiate(EnemyManager.Instance.itemCollection.GetItem(item.id), transform.position, Quaternion.identity);

            if (newItem == null) 
            {
                continue;
            }

            newItem.GetComponent<ItemCrystal>().SetValues(item.itemName, item.id, item.value);
        }
    }

    public override void Damaged(int value)
    {
        if (damageImmune) return;
        
        health -= value;
        if (health < 0)
        {
            Die();
            return;
        }
        
        EnemyManager.Instance.EnemyHit(id, value);
    }

    public override void Die()
    {
        // EnemyManager.Instance.EnemyDied(soulFragment, coin);
        DropItems();

        dieFx.transform.SetParent(null);
        dieFx.transform.position = transform.position;
        dieFx.Play();

        coinFx.transform.SetParent(null);
        coinFx.transform.position = transform.position;
        coinFx.Play();

        soulFx.transform.SetParent(null);
        soulFx.transform.position = transform.position;
        soulFx.Play();

        Destroy(gameObject);
    }
    #endregion
}
