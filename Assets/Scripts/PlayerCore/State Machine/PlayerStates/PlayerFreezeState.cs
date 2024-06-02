namespace PlayerCore.State_Machine
{
    public class PlayerFreezeState : PlayerState
    {
        public PlayerFreezeState(PlayerBehavior player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }
        
        public override bool CanForceChangeState => false;
    }
}