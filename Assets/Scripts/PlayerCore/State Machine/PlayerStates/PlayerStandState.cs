namespace PlayerCore.State_Machine
{
    public class PlayerStandState : PlayerState
    {
        public PlayerStandState(PlayerBehavior player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            Player.Aim(Player.TempMousePosition);
        }
    }
}