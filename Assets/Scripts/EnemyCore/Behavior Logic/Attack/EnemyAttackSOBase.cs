using UnityEngine;

namespace EnemyCore.Behavior_Logic.Attack
{
    public class EnemyAttackSOBase : ScriptableObject
    {
        protected Enemy EnemyRef;
        protected Transform TransformRef;
        protected GameObject GameObjectRef;

        protected Transform Target;

        public virtual void Initialize(GameObject gameObject, Enemy enemy)
        {
            EnemyRef = enemy;
            GameObjectRef = gameObject;
            TransformRef = GameObjectRef.transform;

            Target = EnemyRef.Target;
        }
        
        public virtual void DoEnterLogic() { }
        public virtual void DoExitLogic() { ResetValue(); }
        public virtual void DoFrameUpdateLogic() { }

        public virtual void DoPhysicsUpdateLogic()
        {
            if (Equals(Target, null)) return;
            EnemyRef.Aim(Target.position);
        }
        
        public virtual void DoAnimationTriggerEventLogic(AnimationTriggerType triggerType) { }
        public virtual void ResetValue() { }
    }
}