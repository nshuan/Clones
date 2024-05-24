using UnityEngine;

namespace Character.State_Machine.States
{
    public class CharacterMoveRbState : CharacterMoveState
    {
        public CharacterMoveRbState(Base.Character character, CharacterStateMachine stateMachine) : base(character, stateMachine)
        {
        }
        
        public override void PhysicsUpdate()
        {
            Char.Aim(targetPos);
            Char.MoveRb(direction);
        }
    }
}