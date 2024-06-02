using UnityEngine;

namespace EnemyCore.State_Machine
{
    public class EnemyChaseState : EnemyState
    {
        public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
            
        }

        public override void EnterState()
        {
            base.EnterState();
            
            EnemyRef.ChaseBase.DoEnterLogic();
        }

        public override void ExitState()
        {
            base.ExitState();
            
            EnemyRef.ChaseBase.DoExitLogic();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            
            EnemyRef.ChaseBase.DoFrameUpdateLogic();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            EnemyRef.ChaseBase.DoPhysicsUpdateLogic();
        }

        public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);
            
            EnemyRef.ChaseBase.DoAnimationTriggerEventLogic(triggerType);
        }
    }
}