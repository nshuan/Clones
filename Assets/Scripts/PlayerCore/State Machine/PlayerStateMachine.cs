namespace PlayerCore.State_Machine
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; set; }

        public void Initialize(PlayerState initState)
        {
            CurrentState = initState;
            CurrentState.EnterState();
        }

        public void ChangeState(PlayerState newState)
        {
            if (!Equals(newState, CurrentState) && !CurrentState.CanForceChangeState) return;
            if (Equals(newState, CurrentState) && !CurrentState.CanChangeStateToSelf) return;
            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }
    }
}