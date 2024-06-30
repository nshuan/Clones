using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using EasyButtons;
using Managers;
using PlayerCore;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform player;
    [HideInInspector] public PlayerBehavior playerScript;

    private float timeScale = 1f;
    public float TimeScale
    {
        get {
            return timeScale;
        }
        set {
            timeScale = value;
        }
    }
    
    public bool IsPausing { get; private set; }

    public static event Action<int> OnPlayerLevelUp; 
    
    protected override void Awake()
    {
        base.Awake();
        playerScript = player.GetComponent<PlayerBehavior>();
    }

    #region Game manager
    public void FreezeGame()
    {
        // playerScript.Freeze();
        timeScale = 0.02f;
    }

    public void ThawGame()
    {
        // playerScript.Thaw();
        timeScale = 1f;
    }

    public void SummonBoss()
    {
        if (EnemyManager.Instance.bossing) return;

        EnemyManager.Instance.SpawnBoss();
        BossFightPrepare();
    }

    public void BossFightPrepare()
    {
        TimeScale = 0.1f;
        // playerScript.Freeze();
        CameraManager.Instance.ChangeTarget(EnemyManager.Instance.currentBoss);
        UIManager.Instance.BossFightIntro(2f);
    }

    public void BossFightStart()
    {
        TimeScale = 1f;
        // playerScript.Thaw();
        CameraManager.Instance.ChangeTarget(player);
        UIManager.Instance.BossFightUI(EnemyManager.Instance.currentBossName);

        SoundManager.Instance.PlayBossTheme();
    }

    public void BossFightEnd()
    {
        UIManager.Instance.BossFightUIOff();

        SoundManager.Instance.PlayTheme();
    }

    public void GameOver()
    {
        timeScale = 0.05f;
        PlayerData.UpdateSurvivedTime(UIManager.Instance.StopTimer());
        CoinManager.CurrentCoin += PlayerData.Coin;
        UIManager.Instance.GameOver();
    }
    #endregion

    #region Player data manager
    public void HealPlayer(int value)
    {
        if (player == null) return;
        // playerScript.Heal(value);
        UIManager.Instance.CreateFloatText(player, value, Color.green);
    }

    public void AddScore(int value)
    {
        PlayerData.AddScore(value);

        // UI
        UIManager.Instance.UpdateScore(PlayerData.Score);
    }

    public void AddEnemyKilled(int value, int soul)
    {
        PlayerData.AddEnemyKilled(value);
        PlayerData.AddSoulFragment(soul);
        var currentLevel = PlayerData.Level;
        PlayerData.UpdateLevel(PlayerData.SoulFragment / 8 + 1);
        if (PlayerData.Level > currentLevel) OnPlayerLevelUp?.Invoke(PlayerData.Level);

        if (PlayerData.Level % 5 == 0)
            SummonBoss();

        // UI
        UIManager.Instance.UpdateSoul(PlayerData.SoulFragment);
        UIManager.Instance.UpdateLevel(PlayerData.Level);
    }

    public void AddCoin(int value)
    {
        PlayerData.AddCoin(value);

        // UI
        UIManager.Instance.UpdateCoin(PlayerData.Coin); 
    }
    #endregion

    [Button]
    public void PauseGame()
    {
        IsPausing = true;
        playerScript.Stand();
        timeScale = 0f;
    }

    [Button]
    public void ResumeGame()
    {
        IsPausing = false;
        timeScale = 1f;
    }
}

public static class PlayerData
{
    private static int score = 0;
    private static int soulFragment = 0;
    private static int level = 0;
    private static float survivedTime = 0;
    private static int coin = 0;
    private static int enemyKilled = 0;

    public static int Score { get { return score; } }
    public static int SoulFragment { get { return soulFragment; } }
    public static int Level { get { return level; } }
    public static float SurvivedTime { get { return survivedTime; } }
    public static int Coin { get { return coin; } }
    public static int EnemyKilled { get { return enemyKilled; } }

    public static void AddScore(int value)
    {
        score += value;
    }

    public static void AddSoulFragment(int value)
    {
        soulFragment += value;
    }

    public static void UpdateLevel(int value)
    {
        level = value;
    }

    public static void UpdateSurvivedTime(float value)
    {
        survivedTime = value;
    }

    public static void AddCoin(int value)
    {
        coin += value;
    }

    public static void AddEnemyKilled(int value)
    {
        enemyKilled += value;
    }

    public static void ResetData()
    {
        score = 0;
        soulFragment = 0;
        level = 1;
        survivedTime = 0;
        coin = 0;
        enemyKilled = 0;
    }
}
