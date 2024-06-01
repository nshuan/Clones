using System;
using Characters;
using UnityEngine;

namespace EnemyCore.EnemyData
{
    [Serializable]
    public class EnemyReference
    {
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
        [field: SerializeField] public Enemy EnemyPrefab { get; private set; }
    }
}