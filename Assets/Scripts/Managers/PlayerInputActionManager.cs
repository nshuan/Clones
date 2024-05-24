using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class PlayerInputActionManager : MonoSingleton<PlayerInputActionManager>
    {
        public static event Action<Vector2> onMove;
        public static event Action<Vector2> onAim;
        public static event Action onDash;
        public static event Action<bool> onFire;
        public static event Action onSwitchGun;
        public static event Action onQuitGunReplacement;

        private void OnDestroy()
        {
            onMove = null;
            onAim = null;
            onDash = null;
            onFire = null;
            onSwitchGun = null;
            onQuitGunReplacement = null;
        }
        
        #region Player input
        /// <summary>
        /// Get user's movement input
        /// </summary>
        /// <param name="context"></param>
        public void OnMove(InputAction.CallbackContext context)
        {
            onMove?.Invoke(context.ReadValue<Vector2>());
        }
        
        /// <summary>
        /// Get user's mouse position
        /// </summary>
        /// <param name="context"></param>
        public void OnAim(InputAction.CallbackContext context)
        {
            if (Camera.main != null) onAim?.Invoke(Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()));
        }
    
        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                onDash?.Invoke();
            }
        }
    
        /// <summary>
        /// Called when fire button is pressed
        /// </summary>
        /// <param name="context"></param>
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                onFire?.Invoke(true);
            }
            else if (context.canceled)
            {
                onFire?.Invoke(false);
            }
        }
    
        /// <summary>
        /// Called when switch gun button is pressed
        /// </summary>
        /// <param name="context"></param>
        public void OnSwitchGun(InputAction.CallbackContext context)
        {
            onSwitchGun?.Invoke();
        }
        #endregion   
        
        public void OnQuitGunReplacement(InputAction.CallbackContext context)
        {
            onQuitGunReplacement?.Invoke();
        }
    }
}