using System;
using Character.Interfaces;
using Managers;
using PlayerCore.State_Machine;
using Scripts.PlayerSettings;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerCore
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerBehavior : PhysicalObjectBehavior, IDamageable
    {
        #region State Machine

        public PlayerStateMachine StateMachine { get; protected set; }
        public PlayerStandState IdleState { get; protected set; }
        public PlayerMoveState MoveState { get; protected set; }
        public PlayerDashState DashState { get; protected set; }
        public PlayerBounceState BounceState { get; protected set; }
        public PlayerFreezeState FreezeState { get; protected set; }
        private PlayerAttackState AttackState { get; set; }

        #endregion

        private Rigidbody2D _rb2d;
        
        
        public PlayerCharacterSO PlayerData { get; private set; }
        public int MaxHealth => PlayerData.MaxHealth;
        public int BaseDamage => PlayerData.Damage;
        public int CurrentHealth { get; set; }
        public float TimeScaleResistant => 1f;
        public Gun CurrentGun { get; private set; }

        #region Temporary

        public Vector2 TempMoveDirection { get; set; }
        public Vector2 TempMousePosition { get; set; }

        #endregion
        
        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();

            PlayerData = PlayerManager.Instance.CurrentCharacter;
            CurrentGun = GunManager.Instance.GetGun(PlayerData.DefaultGun);
            
            StateMachine = new PlayerStateMachine();
            IdleState = new PlayerStandState(this, StateMachine);
            MoveState = new PlayerMoveState(this, StateMachine);
            DashState = new PlayerDashState(this, StateMachine);
            BounceState = new PlayerBounceState(this, StateMachine);
            FreezeState = new PlayerFreezeState(this, StateMachine);
            AttackState = new PlayerAttackState(this, StateMachine);
            
            PlayerInputActionManager.onMove += OnMove;
            PlayerInputActionManager.onAim += OnAim;
            PlayerInputActionManager.onDash += OnDash;
            PlayerInputActionManager.onFire += OnFire;
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            StateMachine.CurrentState.FrameUpdate();
            AttackState.FrameUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }


        public void Damage(int value)
        {
            throw new System.NotImplementedException();
        }

        public void Die()
        {
            throw new System.NotImplementedException();
        }

        public override void Move()
        {
            MoveWithRb(_rb2d, TempMoveDirection, PlayerData.Speed, TimeScaleResistant);
        }

        #region PlayerInput

        protected void OnMove(Vector2 direction)
        {
            TempMoveDirection = direction;
            StateMachine.ChangeState(MoveState);
        }
    
        protected void OnAim(Vector2 target)
        {
            TempMousePosition = target;
        }

        protected void OnDash()
        {
            // Dash(TempMousePosition - (Vector2) transform.position, 8f, 3.6f);
        }

        protected void OnFire(bool firing)
        {
            if (firing) AttackState.EnterState();
            else AttackState.ExitState();
        }

        #endregion
    }
}
