using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using EasyButtons;
using UnityEngine;

namespace Managers.Spawner
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Spawner Data", fileName = "EnemySpawnerData", order = 1)]
    public class SpawnerData : ScriptableObject
    {
        [SerializeField] private List<SpawnerThreshold> spawnerThresholds = new List<SpawnerThreshold>();

        public SpawnerThreshold ThresholdData(int level)
        {
            if (spawnerThresholds.Count < 1) return new SpawnerThreshold();
            if (spawnerThresholds.Count == 1) return spawnerThresholds[0];
            
            for (var i = 0; i < spawnerThresholds.Count - 1; i++)
            {
                if (level <= spawnerThresholds[i + 1].LevelThreshold) return spawnerThresholds[i];
            }
            return spawnerThresholds[^1];
        }
        
        #region Inspector Buttons

        [Button]
        private void SortThreshold()
        {
            spawnerThresholds.Sort((threshold1, threshold2) => 
                threshold1.LevelThreshold.CompareTo(threshold2.LevelThreshold));    
            
            NormalizeSpawnChance();
        }

        [Button]
        private void NormalizeSpawnChance()
        {
            foreach (var threshold in spawnerThresholds)
            {
                var totalRate = threshold.SpawnableTypes.Sum(st => st.Chance);
                foreach (var st in threshold.SpawnableTypes)
                {
                    st.NormalizeChance(totalRate);
                }
            }
        }

        #endregion
    }

    [Serializable]
    public class SpawnerThreshold
    {
        [field: SerializeField] public int LevelThreshold { get; private set; }
        [field: SerializeField] public int MaxSpawnedEnemy { get; private set; } = 4;
        [field: SerializeField] public float SpawnRate { get; private set; } = 1; // Number of enemy spawned every second.
        [field: SerializeField] public List<SpawnChance> SpawnableTypes { get; private set; } = new();
        [field: SerializeField] public bool IsBossLevel { get; private set; }

        [Serializable]
        public class SpawnChance
        {
            [field: SerializeField] public EnemyType EnemyType { get; private set; }
            [field: SerializeField] public float Chance { get; private set; } = 1;

            public void NormalizeChance(float scale)
            {
                Chance /= scale;
            }
        }
    }
}