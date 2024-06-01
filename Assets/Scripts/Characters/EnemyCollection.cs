using System.Collections.Generic;
using System.Linq;
using EasyButtons;
using EnemyCore.EnemyData;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Enemy Collection", fileName = "EnemyCollection", order = 1)]
    public class EnemyCollection : ScriptableObject
    {
        private List<EnemyStats> staticEnemyTypes = new List<EnemyStats>();
        [SerializeField] private List<EnemyStats> enemyTypes = new List<EnemyStats>();
        
        public int enemyCount => enemyTypes.Count;
        
        
        public EnemyStats GetEnemyStat(EnemyType type)
        {
            return enemyTypes.FirstOrDefault(enemy => enemy.EnemyType == type);
        }

        public EnemyStats GetEnemyStat(int index)
        {
            return enemyTypes[index];
        }

        [Button]
        private void SaveEnemyData()
        {
            staticEnemyTypes = new List<EnemyStats>();
            foreach (var e in enemyTypes)
            {
                staticEnemyTypes.Add(e);
            }
        }
    }

    public enum EnemyType
    {
        Normie,
        Runner,
        Hunter,
        Soldier,
        Tanker
    }
}