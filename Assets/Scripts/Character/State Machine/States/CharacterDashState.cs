using UnityEngine;

namespace Character.State_Machine.States
{
    public class CharacterDashState : CharacterState
    {
        protected Vector2 slipTarget;
        protected float slipDuration = 0f;
        protected float slipCounter = 0f;
        protected float dashCooldown = 3f;
        protected float dashCdCounter = 0f;
        protected float dashSpeedScale;

        protected Vector2 targetPos;
        
        public CharacterDashState(Base.Character character, CharacterStateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void PhysicsUpdate()
        {
            Char.Aim(targetPos);

            if (Vector2.Distance(Char.transform.position, slipTarget) > 0.1f && slipCounter < slipDuration)
            {
                slipCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + Char.TimeScaleResistant, 0f, 1f);
                // transform.position = Vector2.SmoothDamp(transform.position, slipTarget, ref slipVel, 0.2f * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f));
                Char.transform.position = Vector2.MoveTowards(Char.transform.position, 
                    slipTarget, 0.02f * Char.Speed * dashSpeedScale * Mathf.Clamp(GameManager.Instance.TimeScale + Char.TimeScaleResistant, 0f, 1f));
                return;
            }
            StateMachine.ChangeState(Char.MoveState);
        }

        public void SetupTemp(Vector2 target, float length, float dashSp)
        {
            if (dashCdCounter < dashCooldown) return;

            var charPos = (Vector2)Char.transform.position;
            var direction = target - charPos;
            targetPos = target;
            slipTarget = charPos + direction.normalized * length;
            slipDuration = 1.5f;
            slipCounter = 0f;
            dashCdCounter = 0f;
            dashSpeedScale = dashSp;
        }
    }
}