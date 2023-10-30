using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; set; }

    #region Enemy types
    public static EnemyStats normie = new EnemyStats("Normie", 60, 1, 2f, 10f, 1f, Color.white, true, GunCollection.pistol);
    public static EnemyStats runner = new EnemyStats("Runner", 50, 1, 6f, 0f, 0.8f, Color.yellow, false, null);
    public static EnemyStats hunter = new EnemyStats("Hunter", 80, 1, 4f, 8f, 1.2f, Color.red, true, GunCollection.shotgun);
    public static EnemyStats soldier = new EnemyStats("Soldier", 80, 1, 4f, 12f, 1.2f, Color.cyan, true, GunCollection.smg);

    public static EnemyStats tanker = new EnemyStats("Tanker", 140, 4, 2f, 7f, 1.6f, Color.green, true, GunCollection.cherryCanon);
    #endregion
    
    private Transform player;
    [SerializeField] private GameObject enemyPrefab;

    private List<EnemyStats> enemyTypes = new List<EnemyStats>();
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

    void Awake()
    {
        Instance = this;

        enemyTypes.Add(normie.SetupFragments(1, Random.Range(3, 6)));
        enemyTypes.Add(runner.SetupFragments(2, Random.Range(2, 4)));
        enemyTypes.Add(hunter.SetupFragments(2, Random.Range(8, 16)));
        enemyTypes.Add(soldier.SetupFragments(1, Random.Range(4, 8)));
        enemyTypes.Add(tanker.SetupFragments(4, Random.Range(6, 10)));

        enemyBuffer = new List<EnemyBehavior>();
        spawnThreshold = new float[enemyTypes.Count];
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
                                    newStats.GetGun()   )
                    .SetupItems(    0.15f, 0.1f );

            enemyBuffer[i] = newScript;

            spawnCdCounter = 0f;
            break;
        }
    }

    public EnemyStats GetRandomEnemy()
    {
        float k = Random.Range(0f, 1f);
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            if (k <= spawnThreshold[i]) 
            {
                k = i;
                break;
            }
        }
        return enemyTypes[(int) k];
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

public class EnemyStats
{
    private string name;
    private int maxHealth;
    private int soulFragment;
    private int coin;
    private int baseDamage;
    private float speed;
    private float attackRange;
    private float sizeScale;
    private Color color;
    private bool hasGun;
    private Gun gun;

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
        this.gun = gun;
    }

    public EnemyStats SetupFragments(int soul, int coin)
    {
        this.soulFragment = soul;
        this.coin = coin;

        return this;
    }

    public string GetName()
    {
        return name;
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

    public Gun GetGun()
    {
        return gun;
    }
}
