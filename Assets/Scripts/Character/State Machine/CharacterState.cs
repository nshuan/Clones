
namespace Character.State_Machine
{
    using Character.Base;
    
    public class CharacterState
    {
        protected Character Char;
        protected CharacterStateMachine StateMachine;

        public CharacterState(Character character, CharacterStateMachine stateMachine)
        {
            Char = character;
            StateMachine = stateMachine;
        }

        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void FrameUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void AnimationTriggerEvent(CharacterTriggerType triggerType) { }
    }
}