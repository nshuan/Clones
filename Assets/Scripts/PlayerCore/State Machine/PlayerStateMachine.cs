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
            if (!CurrentState.CanForceChangeState) return;
            if (Equals(newState, CurrentState) && !CurrentState.CanChangeStateToSelf) return;
            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }

        public void ChangeState(PlayerState sender, PlayerState newState)
        {
            if (Equals(newState, CurrentState) && !CurrentState.CanChangeStateToSelf) return;
            if (!Equals(sender, CurrentState) &&
                (Equals(sender, CurrentState) || !CurrentState.CanForceChangeState)) return;
            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }
    }
}