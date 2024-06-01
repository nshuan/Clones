using System.Collections.Generic;
using System.Linq;
using EasyButtons;
using EnemyCore;
using EnemyCore.EnemyData;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Enemy Collection", fileName = "EnemyCollection", order = 1)]
    public class EnemyCollection : ScriptableObject
    {
        [SerializeField] private List<EnemyReference> enemyTypes = new List<EnemyReference>();
        
        public int enemyCount => enemyTypes.Count;
        
        
        public Enemy GetEnemy(EnemyType type)
        {
            return enemyTypes.FirstOrDefault(enemy => enemy.EnemyType == type)?.EnemyPrefab;
        }

        public Enemy GetEnemy(int index)
        {
            return enemyTypes[index].EnemyPrefab;
        }

        // [Button]
        // private void SaveEnemyData()
        // {
        //
        // }
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