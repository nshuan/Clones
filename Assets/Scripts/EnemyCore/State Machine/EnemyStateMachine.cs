namespace EnemyCore.State_Machine
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentState { get; set; }

        public void Initialize(EnemyState initState)
        {
            CurrentState = initState;
            CurrentState.EnterState();
        }

        public void ChangeState(EnemyState newState)
        {
            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }
    }
}