using UnityEngine;

namespace EnemyCore.State_Machine.EnemyStates
{
    public class EnemyBounceState : EnemyState
    {
        private Transform _target;

        private Vector2 _slipTarget;
        private float _slipDuration = 0.5f;
        private float _slipCounter = 0f;
        private float _timeScaleResistant = 1f;
        
        public EnemyBounceState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
            _target = GameManager.Instance.player;
        }

        public override void EnterState()
        {
            base.EnterState();

            var enemyPos = EnemyRef.transform.position;
            _slipTarget = enemyPos + (enemyPos - _target.position).normalized;
            _slipCounter = 0f;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            if (_slipCounter < _slipDuration)
            {
                var slipVel = Vector2.zero;
                _slipCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + _timeScaleResistant, 0f, 1f);
                EnemyRef.transform.position = Vector2.SmoothDamp(EnemyRef.transform.position, _slipTarget, ref slipVel, 0.2f * Mathf.Clamp(GameManager.Instance.TimeScale + _timeScaleResistant, 0f, 1f));
                return;
            }
            
            EnemyRef.StateMachine.ChangeState(EnemyRef.ChaseState);
        }
    }
}