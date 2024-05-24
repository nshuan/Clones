using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Enemy Collection", fileName = "EnemyCollection", order = 1)]
    public class EnemyCollection : ScriptableObject
    {
        [SerializeField] private List<EnemyStats> enemyTypes = new List<EnemyStats>();
        public int enemyCount => enemyTypes.Count;
        
        
        public EnemyStats GetEnemyStat(EnemyType type)
        {
            return enemyTypes.FirstOrDefault(enemy => enemy.GetEnemyType() == type);
        }

        public EnemyStats GetEnemyStat(int index)
        {
            return enemyTypes[index];
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