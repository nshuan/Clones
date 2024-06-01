using UnityEngine;

namespace EnemyCore.Behavior_Logic.Chase
{
    public class EnemyChaseSOBase : ScriptableObject
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

        public virtual void DoFrameUpdateLogic()
        {
            if (EnemyRef.IsWithinStrikingDistance) EnemyRef.StateMachine.ChangeState(EnemyRef.AttackState);
        }

        public virtual void DoPhysicsUpdateLogic()
        {
            if (Equals(Target, null)) return;
            
            EnemyRef.Aim(Target.position);
            EnemyRef.Move(Target.position);
        }
        
        public virtual void DoAnimationTriggerEventLogic(AnimationTriggerType triggerType) { }
        public virtual void ResetValue() { }
    }
}