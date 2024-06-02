using UnityEngine;

namespace EnemyCore.Behavior_Logic.Chase
{
    [CreateAssetMenu(fileName = "EnemyDirectChase", menuName = "Scriptable Objects/Enemy Logic/Chase Logic/Direct Chase", order = 1)]
    public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
    {
        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
        }

        public override void DoFrameUpdateLogic()
        {
            base.DoFrameUpdateLogic();
        }

        public override void DoPhysicsUpdateLogic()
        {
            base.DoPhysicsUpdateLogic();
            
            if (Target is null) return;
            
            EnemyRef.Aim(Target.position);
            EnemyRef.Move();
        }

        public override void DoAnimationTriggerEventLogic(AnimationTriggerType triggerType)
        {
            base.DoAnimationTriggerEventLogic(triggerType);
        }

        public override void ResetValue()
        {
            base.ResetValue();
        }
    }
}