using UnityEngine;

namespace PlayerCore.State_Machine
{
    public class PlayerDashState : PlayerState
    {
        private const float DashDuration = 0.2f;
        private float _dashCounter = 0f;

        public Vector2 Direction;
        
        public PlayerDashState(PlayerBehavior player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            _dashCounter = 0f;

            if (Direction.magnitude < 0.01f)
                Direction = Player.transform.forward;
        }

        public override void ExitState()
        {
            base.ExitState();
            
            Player.Stand();
        }

        public override void FrameUpdate()
        {
            if (_dashCounter >= DashDuration) Player.StateMachine.ChangeState(this, Player.IdleState);

            _dashCounter += Time.deltaTime;
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            Player.Move(Direction, 4 * Player.PlayerData.Speed);
        }
        
        public override bool CanForceChangeState => false;
        public override bool CanChangeStateToSelf => false;
    }
}