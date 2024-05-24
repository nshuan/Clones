using System;
using Managers;
using System.Collections;
using System.Collections.Generic;
using Characters;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    private const string EnemyCollectionPath = "EnemyCollection";
    
    private Transform player;
    [SerializeField] private GameObject enemyPrefab;
    
    private EnemyCollection enemyCollection;
    private float[] spawnThreshold;

    private int maxEnemy = 0;
    private List<EnemyBehavior> enemyBuffer;

    private float spawnCooldown = 2f;
    private float spawnCdCounter = 0f;

    [HideInInspector] public bool bossing = false;
    [HideInInspector] public Transform currentBoss;
    [HideInInspector] public string currentBossName;

    public ItemCollection itemCollection;
    public BossCollection bossCollection;

    protected override void Awake()
    {
        base.Awake();

        enemyCollection = Resources.Load<EnemyCollection>(EnemyCollectionPath);
        
        enemyCollection.GetEnemyStat(EnemyType.Normie).SetupFragments(1, Random.Range(3, 6));
        enemyCollection.GetEnemyStat(EnemyType.Runner).SetupFragments(2, Random.Range(2, 4));
        enemyCollection.GetEnemyStat(EnemyType.Hunter).SetupFragments(2, Random.Range(8, 16));
        enemyCollection.GetEnemyStat(EnemyType.Soldier).SetupFragments(1, Random.Range(4, 8));
        enemyCollection.GetEnemyStat(EnemyType.Tanker).SetupFragments(4, Random.Range(6, 10));

        enemyBuffer = new List<EnemyBehavior>();
        spawnThreshold = new float[enemyCollection.enemyCount];
    }

    void Start()
    {
        player = GameManager.Instance.player;

        CalculateSpawnRate(PlayerData.Level);
    }

    void Update()
    {
        if (player == null) return;

        if (bossing) return;

        if (spawnCdCounter < spawnCooldown)
        {
            spawnCdCounter += Time.deltaTime * GameManager.Instance.TimeScale;
            return;
        }

        for (int i = 0; i < enemyBuffer.Count; i++)
        {
            if (enemyBuffer[i] != null) continue;

            // Camera size = 16f x 9f
            // Max radius = 28f
            EnemyStats newStats = GetRandomEnemy();
            Vector2 offset = Random.insideUnitCircle * 12f;
            EnemyBehavior newScript = Instantiate(enemyPrefab, (Vector2) Camera.main.transform.position + new Vector2(16f * Mathf.Sign(offset.x), 16f * Mathf.Sign(offset.y)) + offset, Quaternion.identity)
                .GetComponent<EnemyBehavior>();
            newScript.SetupStats(   newStats.GetName(),
                                    i,
                                    newStats.GetHP() + 10 * PlayerData.Level,
                                    newStats.GetSoulFragment(),
                                    newStats.GetCoin() + PlayerData.Level,
                                    newStats.GetDamage(),
                                    newStats.GetSpeed(),
                                    newStats.GetAttackRange(),
                                    newStats.GetSizeScale(),
                                    newStats.GetColor(),
                                    GunManager.Instance.GetGun(newStats.GetGunType())   )
                    .SetupItems(    0.15f, 0.1f );

            enemyBuffer[i] = newScript;

            spawnCdCounter = 0f;
            break;
        }
    }

    public EnemyStats GetRandomEnemy()
    {
        float k = Random.Range(0f, 1f);
        for (int i = 0; i < enemyCollection.enemyCount; i++)
        {
            if (k <= spawnThreshold[i]) 
            {
                k = i;
                break;
            }
        }
        return enemyCollection.GetEnemyStat((int) k);
    }

    // IMPORTANT!
    // The enemy list must also be sorted by enemy power.
    public void CalculateSpawnRate(int level)
    {
        spawnThreshold[0] = 0.6f;
        spawnThreshold[1] = 0.8f;
        spawnThreshold[2] = 0.9f;
        spawnThreshold[3] = 0.95f;
        spawnThreshold[4] = 1.0f;
        
        maxEnemy = 4;
        if (maxEnemy > enemyBuffer.Count)
        {
            for (int i = enemyBuffer.Count; i < maxEnemy; i++)
            {
                enemyBuffer.Add(null);
            }
        }
        else if (maxEnemy < enemyBuffer.Count)
        {
            for (int i = enemyBuffer.Count - 1; i >= maxEnemy; i++)
            {
                enemyBuffer.RemoveAt(i);
            }
        }

        spawnCooldown = 1.5f;
        return;
    }

    public void SpawnBoss()
    {
        bossing = true;

        currentBoss = Instantiate(bossCollection.GetBoss(0), (Vector2) GameManager.Instance.player.transform.position + 20f * Random.insideUnitCircle.normalized, Quaternion.identity).transform;
        currentBossName = currentBoss.name;
    }

    public void BossDied(int soul)
    {
        bossing = false;
        GameManager.Instance.BossFightEnd();
        GameManager.Instance.AddEnemyKilled(1, soul);
    }

    public void EnemyHit(int id, int damaged)
    {
        UIManager.Instance.CreateFloatText(enemyBuffer[id].transform, damaged, Color.cyan);
    }

    public void EnemyDied(int id, int soul, int coin)
    {
        GameManager.Instance.AddScore(1);
        GameManager.Instance.AddEnemyKilled(1, soul);
        GameManager.Instance.AddCoin(coin);
    }
}

[Serializable]
public class EnemyStats
{
    [SerializeField] private string name;
    [SerializeField] private EnemyType type;
    [SerializeField] private int maxHealth;
    [SerializeField] private int baseDamage;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float sizeScale;
    [SerializeField] private Color color;
    [SerializeField] private bool hasGun;
    [SerializeField] private GunType gunType;
    private int soulFragment;
    private int coin;

    public EnemyStats(string name, int maxHealth, int damage, float speed, float attackRange, float sizeScale, Color color, bool hasGun, Gun gun)
    {
        this.name = name;
        this.maxHealth = maxHealth;
        this.baseDamage = damage;
        this.speed = speed;
        this.attackRange = attackRange;
        this.sizeScale = sizeScale;
        this.color = color;
        this.hasGun = hasGun;
        this.gunType = gun.GetGunType();
    }

    public EnemyStats SetupFragments(int soul, int coin)
    {
        this.soulFragment = soul;
        this.coin = coin;

        return this;
    }

    public string GetName()
    {
        return type.ToString();
    }

    public EnemyType GetEnemyType()
    {
        return type;
    }

    public int GetHP()
    {
        return maxHealth;
    }

    public int GetSoulFragment()
    {
        return soulFragment;
    }

    public int GetCoin()
    {
        return coin;
    }

    public int GetDamage()
    {
        return baseDamage;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetAttackRange()
    {
        return attackRange;
    }
    
    public float GetSizeScale()
    {
        return sizeScale;
    }

    public Color GetColor()
    {
        return color;
    }

    public bool HasGun()
    {
        return hasGun;
    }

    public GunType GetGunType()
    {
        return gunType;
    }
}
