using EnemyCore;
using EnemyCore.Behavior_Logic.Attack;
using EnemyCore.Boss;
using UnityEngine;

namespace Characters.Bosses
{
    public class TheDotNew : Enemy
    {
        [SerializeField] private EnemyAttackSOBase attackLogic1;
        [SerializeField] private EnemyAttackSOBase attackLogic2;

        public EnemyAttackSOBase AttackPhase1 { get; private set; }
        public EnemyAttackSOBase AttackPhase2 { get; private set; }
        
        protected override void Start()
        {
            AttackPhase1 = Instantiate(attackLogic1);
            AttackPhase2 = Instantiate(attackLogic2);
            AttackBase = attackLogic1;
            
            ChaseBase.Initialize(gameObject, this);
            AttackBase.Initialize(gameObject, this);
            StateMachine.Initialize(ChaseState);
            
            MaxHealth = Stats.MaxHealth;
            CurrentHealth = MaxHealth;
        }

        public void Stand(float duration)
        {
            
        }
        
    }
}