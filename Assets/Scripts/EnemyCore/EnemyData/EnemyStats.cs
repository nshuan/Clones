using System;
using Characters;
using UnityEngine;

namespace EnemyCore.EnemyData
{
    [Serializable]
    public class EnemyStats
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
        [field: SerializeField] public EnemyClass EnemyClass { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public int BaseDamage { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float SizeScale { get; private set; }
        [field: SerializeField] public Color VisualColor { get; private set; }
        [field: SerializeField] public bool HasGun { get; private set; }
        [field: SerializeField] public GunType GunType { get; private set; }
        [field: SerializeField] public int SoulFragment { get; private set; }
        [field: SerializeField] public int Coin { get; private set; }

        public EnemyStats SetupFragments(int soul, int coin)
        {
            this.SoulFragment = soul;
            this.Coin = coin;

            return this;
        }
    }

    public enum EnemyClass
    {
        Gunner,
        Unarmed
    }
}