
using UnityEngine;

namespace PlayerCore.State_Machine
{
    public class PlayerBounceState : PlayerState
    {
        private Vector2 _bounceDirection;
        private Vector2 _bounceTarget;
        private float _bounceDuration = 0.5f;
        private float _bounceCounter = 0.5f;
        
        public PlayerBounceState(PlayerBehavior player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            var sizeRate = 1f;
            if (Player.TempHitObject is not null)
                sizeRate = Player.transform.localScale.magnitude / Player.TempHitObject.localScale.magnitude;
            var newDirection = (Vector2)(Player.transform.position - Player.TempHitObject.position).normalized * 2 * sizeRate;
            _bounceDirection = _bounceDirection * (_bounceDuration - _bounceCounter) / _bounceDuration + newDirection;
            _bounceTarget = (Vector2)Player.transform.position + _bounceDirection;
            _bounceDuration = 0.5f * sizeRate;
            _bounceCounter = 0f;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            if (_bounceCounter < _bounceDuration)
            {
                _bounceCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + Player.TimeScaleResistant, 0f, 1f);
                var currentVel = Vector2.zero;
                Player.transform.position = Vector2.SmoothDamp(Player.transform.position, _bounceTarget, ref currentVel, 0.2f * Mathf.Clamp(GameManager.Instance.TimeScale + Player.TimeScaleResistant, 0f, 1f));
            }
        }
    }
}