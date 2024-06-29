namespace PlayerCore.State_Machine
{
    public abstract class PlayerState
    {
        protected PlayerBehavior Player;
        protected PlayerStateMachine StateMachine;
        public virtual bool CanForceChangeState { get; protected set; } = true;
        public virtual bool CanChangeStateToSelf => true;

        public PlayerState(PlayerBehavior player, PlayerStateMachine stateMachine)
        {
            Player = player;
            StateMachine = stateMachine;
        }

        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void FrameUpdate() { }
        public virtual void PhysicsUpdate() { }
    }
}