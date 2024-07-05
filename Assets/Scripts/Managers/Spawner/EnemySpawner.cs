using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using EnemyCore;
using EnemyCore.EnemyData;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SpawnerData spawnerData;
        [SerializeField] private BossSpawner bossSpawner;

        private int _currentLevel = 1;
        private SpawnerThreshold _currentThresholdData = new SpawnerThreshold();

        private int _activeEnemy = 0;
        private float _spawnCd;
        private float _spawnCdCounter = 0f;
        
        private void Awake()
        {
            OnPlayerLevelUp(0);
            
            GameManager.OnPlayerLevelUp += OnPlayerLevelUp;
            EnemyManager.OnEnemyDied += OnEnemyDied;
        }

        private void OnDestroy()
        {
            GameManager.OnPlayerLevelUp -= OnPlayerLevelUp;
            EnemyManager.OnEnemyDied -= OnEnemyDied;
        }

        private void Update()
        {
            if (_currentThresholdData.IsBossLevel)
            {
                return;
            }
            if (_activeEnemy > _currentThresholdData.MaxSpawnedEnemy) return;
            
            if (_spawnCdCounter < _spawnCd)
            {
                _spawnCdCounter += Time.deltaTime * GameManager.Instance.TimeScale;
                return;
            }
            
            // Camera size = 16f     x 9f
            // Max radius = 28f
            var newEnemy = GetRandomEnemy(_currentThresholdData);
            Vector2 offset = Random.insideUnitCircle * 12f;
            Instantiate(newEnemy, (Vector2) Camera.main.transform.position + new Vector2(16f * Mathf.Sign(offset.x), 16f * Mathf.Sign(offset.y)) + offset, Quaternion.identity)
                .GetComponent<Enemy>()
                .InitEnemy(PlayerData.Level);

            _spawnCdCounter = 0f;
            _activeEnemy += 1;
        }
        
        public static Enemy GetRandomEnemy(SpawnerThreshold spawnerThreshold)
        {
            var totalRate = -spawnerThreshold.SpawnableTypes.Sum(st => st.Chance);
            var cumulativeRate = 0f;
            var k = Random.Range(0f, 1f);

            foreach (var st in spawnerThreshold.SpawnableTypes)
            {
                cumulativeRate += st.Chance;
                if (k <= cumulativeRate)
                {
                    return EnemyManager.Instance.EnemyCollection.GetEnemy(st.EnemyType);
                }
            }

            return EnemyManager.Instance.EnemyCollection.GetEnemy(EnemyType.Normie);
        }

        private void OnPlayerLevelUp(int currentLevel)
        {
            _currentLevel = currentLevel;
            _currentThresholdData = spawnerData.ThresholdData(_currentLevel);
            _spawnCd = 1 / _currentThresholdData.SpawnRate;

            if (_currentThresholdData.IsBossLevel)
            {
                bossSpawner.DoSpawn(_currentThresholdData);
            }
        }
        
        private void OnEnemyDied(Enemy obj)
        {
            _activeEnemy--;
        }
    }
}