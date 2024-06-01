using UnityEngine;

namespace EnemyCore.State_Machine.EnemyStates
{
    public class EnemyIdleState: EnemyState
    {
        private Transform _target;
        
        public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
            _target = enemy.Target;
        }

        public override void PhysicsUpdate()
        {
            if (_target is null) return;
            EnemyRef.Aim(_target.position);
        }
    }
}