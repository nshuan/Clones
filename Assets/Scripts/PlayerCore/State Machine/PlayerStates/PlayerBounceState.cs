namespace PlayerCore.State_Machine
{
    public class PlayerBounceState : PlayerState
    {
        public PlayerBounceState(PlayerBehavior player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }
        
        public override bool CanForceChangeState => false;
    }
}