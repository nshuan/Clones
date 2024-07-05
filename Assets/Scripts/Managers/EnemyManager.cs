using System;
using Managers;
using System.Collections;
using System.Collections.Generic;
using Characters;
using Core;
using EnemyCore;
using EnemyCore.EnemyData;
using Managers.Spawner;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    private const string EnemyCollectionPath = "EnemyCollection";

    [SerializeField] private EnemySpawner enemySpawner;
    public EnemyCollection EnemyCollection { get; private set; }

    [HideInInspector] public bool bossing = false;
    [HideInInspector] public Transform currentBoss;
    [HideInInspector] public string currentBossName;

    public ItemCollection itemCollection;
    public BossCollection bossCollection;

    public static event Action<Enemy> OnEnemyDied;
    
    protected override void Awake()
    {
        base.Awake();

        EnemyCollection = Resources.Load<EnemyCollection>(EnemyCollectionPath);
        
        // EnemyCollection.GetEnemyStat(EnemyType.Normie).SetupFragments(1, Random.Range(3, 6));
        // EnemyCollection.GetEnemyStat(EnemyType.Runner).SetupFragments(2, Random.Range(2, 4));
        // EnemyCollection.GetEnemyStat(EnemyType.Hunter).SetupFragments(2, Random.Range(8, 16));
        // EnemyCollection.GetEnemyStat(EnemyType.Soldier).SetupFragments(1, Random.Range(4, 8));
        // EnemyCollection.GetEnemyStat(EnemyType.Tanker).SetupFragments(4, Random.Range(6, 10));
    }

    public void EnemyHit(int id, int damaged)
    {
        
    }

    public void EnemyDied(Enemy enemy)
    {
        OnEnemyDied?.Invoke(enemy);
        GameManager.Instance.AddScore(1);
        GameManager.Instance.AddEnemyKilled(1, enemy.Stats.SoulFragment);
        GameManager.Instance.AddCoin(enemy.Stats.Coin);
    }
    
    #region Bossing

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

    #endregion
}
