using Managers;
using Character.State_Machine.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    using Base;
    
    public class Player : Character
    {
        private Vector2 mousePos;
         
        protected override void Awake()
        {
            base.Awake();
            MoveState = new CharacterMoveRbState(this, StateMachine);
            
            PlayerInputActionManager.onMove += OnMove;
            PlayerInputActionManager.onAim += OnAim;
            PlayerInputActionManager.onDash += OnDash;
            PlayerInputActionManager.onFire += OnFire;
            PlayerInputActionManager.onSwitchGun += OnSwitchGun;
            PlayerInputActionManager.onQuitGunReplacement += OnQuitGunReplacement;
        }
        
        #region Player input
        protected void OnMove(Vector2 direction)
        {
            MoveState.SetTargetPos(mousePos);
            MoveState.SetDirection(direction);
        }
    
        protected void OnAim(Vector2 target)
        {
            mousePos = target;
        }

        protected void OnDash()
        {
            Dash(mousePos - (Vector2) transform.position, 8f, 3.6f);
        }

        protected void OnFire(bool firing)
        {
            // this.isFiring = firing;
        }
    
        protected void OnSwitchGun()
        {
            // When choosing gun slot to replace new gun, player can press Q to quit choosing if they do not want to change guns
            if (UIManager.Instance.replacingGun)
            {
                UIManager.Instance.replaceGunBoard.EndChoosing();
                return;
            }

            // inventory.SwitchNextGun();
            // UpdateGun();
        }

        protected void OnQuitGunReplacement()
        {
            if (UIManager.Instance.replacingGun)
            {
                UIManager.Instance.replaceGunBoard.EndChoosing();
                return;
            }
        }
        #endregion
    }
}