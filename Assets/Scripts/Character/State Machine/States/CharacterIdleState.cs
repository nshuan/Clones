namespace Character.State_Machine.States
{
    public class CharacterIdleState : CharacterState
    {
        public CharacterIdleState(Base.Character character, CharacterStateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void PhysicsUpdate()
        {
        }
    }
}