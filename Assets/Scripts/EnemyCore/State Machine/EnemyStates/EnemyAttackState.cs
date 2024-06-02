using Core.ObjectPooling;
using Managers;
using UnityEngine;

namespace EnemyCore.State_Machine
{
    public class EnemyAttackState : EnemyState
    {
        
        
        public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
            
        }

        public override void EnterState()
        {
            base.EnterState();
            
            EnemyRef.AttackBase.DoEnterLogic();
        }

        public override void ExitState()
        {
            base.ExitState();
            
            EnemyRef.AttackBase.DoExitLogic();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            
            EnemyRef.AttackBase.DoFrameUpdateLogic();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            EnemyRef.AttackBase.DoPhysicsUpdateLogic();
        }

        public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);
            
            EnemyRef.AttackBase.DoAnimationTriggerEventLogic(triggerType);
        }
    }
}