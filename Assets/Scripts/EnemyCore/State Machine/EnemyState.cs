
namespace EnemyCore.State_Machine
{
    public abstract class EnemyState
    {
        protected Enemy EnemyRef;
        protected EnemyStateMachine StateMachine;

        public EnemyState(Enemy enemy, EnemyStateMachine stateMachine)
        {
            EnemyRef = enemy;
            StateMachine = stateMachine;
        }

        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void FrameUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void AnimationTriggerEvent(AnimationTriggerType triggerType) { }
    }
}