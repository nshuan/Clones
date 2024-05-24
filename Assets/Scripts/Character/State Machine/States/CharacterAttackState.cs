using Character.Base;

namespace Character.State_Machine.States
{
    public class CharacterAttackState : CharacterState
    {
        public CharacterAttackState(Base.Character character, CharacterStateMachine stateMachine) : base(character, stateMachine)
        {
        }
        
        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void AnimationTriggerEvent(CharacterTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);
        }
    }
}