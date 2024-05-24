namespace Character.State_Machine
{
    using Character.Base;
    
    public class CharacterStateMachine
    {
        public CharacterState CurrentState { get; set; }

        public void Initialize(CharacterState initState)
        {
            CurrentState = initState;
            CurrentState.EnterState();
        }

        public void ChangeState(CharacterState newState)
        {
            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }
    }
}