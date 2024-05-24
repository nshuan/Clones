using Character.Base;
using UnityEngine;

namespace Character.State_Machine.States
{
    public class CharacterMoveState : CharacterState
    {
        protected Vector2 targetPos;
        protected Vector2 direction;
        
        public CharacterMoveState(Base.Character character, CharacterStateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
        }

        public override void PhysicsUpdate()
        {
            Char.Aim(targetPos);
            Char.Move(targetPos);
        }

        public override void AnimationTriggerEvent(CharacterTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);
        }

        public void SetTargetPos(Vector2 target)
        {
            this.targetPos = target;
        }
        
        public void SetDirection(Vector2 direction)
        {
            this.direction = direction;
        }
    }
}