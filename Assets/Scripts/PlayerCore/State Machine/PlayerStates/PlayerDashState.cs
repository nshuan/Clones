namespace PlayerCore.State_Machine
{
    public class PlayerDashState : PlayerState
    {
        public PlayerDashState(PlayerBehavior player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override bool CanForceChangeState => false;
        public override bool CanChangeStateToSelf => false;
    }
}