namespace PlayerCore.State_Machine
{
    public class PlayerMoveState : PlayerState
    {
        public PlayerMoveState(PlayerBehavior player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            Player.Aim(Player.TempMousePosition);
            Player.Move();
        }
    }
}