using System;
using Character.Interfaces;
using Effects;
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
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        
        public PlayerCharacterSO PlayerData { get; private set; }
        public int MaxHealth => PlayerData.MaxHealth;
        public int BaseDamage => PlayerData.Damage;
        public int CurrentHealth { get; set; }
        public float TimeScaleResistant => 1f;
        public Gun CurrentGun { get; private set; }
        private Gun SecondGun { get; set; }

        #region Temporary

        public Vector2 TempMoveDirection { get; set; }
        public Vector2 TempMousePosition { get; set; }
        public Transform TempHitObject { get; set; }
        public override Vector2 Velocity => _rb2d.velocity;

        #endregion

        public static event Action<Gun> OnPlayerGunSwitched; 
        
        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();

            PlayerData = PlayerManager.Instance.CurrentCharacter;
            CurrentGun = GunManager.Instance.GetGun(PlayerData.DefaultGun);
            SecondGun = GunManager.Instance.GetRandomGunExcept(CurrentGun);
            
            // Setup visual
            spriteRenderer.sprite = PlayerData.AvatarSprite;
            spriteRenderer.color = PlayerData.Color;
            
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
            PlayerInputActionManager.onSwitchGun += OnSwitchGun;
        }

        private void OnDestroy()
        {
            PlayerInputActionManager.onMove -= OnMove;
            PlayerInputActionManager.onAim -= OnAim;
            PlayerInputActionManager.onDash -= OnDash;
            PlayerInputActionManager.onFire -= OnFire;
            PlayerInputActionManager.onSwitchGun -= OnSwitchGun;

            OnPlayerGunSwitched = null;
        }

        private void Start()
        {
            CurrentHealth = MaxHealth;
            
            StateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            if (GameManager.Instance.IsPausing) return;
            
            StateMachine.CurrentState.FrameUpdate();
            AttackState.FrameUpdate();
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.IsPausing) return;
            
            StateMachine.CurrentState.PhysicsUpdate();
            BounceState.PhysicsUpdate();
        }


        public void Damage(int value)
        {
            if (Equals(StateMachine.CurrentState, DashState)) return;
            
            UIManager.Instance.CreateFloatText(transform, value, Color.red);
            // Decrease health
            CurrentHealth -= value;
            UIManager.Instance.UpdateHealthBar(CurrentHealth, MaxHealth);
            
            if (CurrentHealth < 0)
                Die();
        }

        public void Die()
        {
            GameManager.Instance.GameOver();
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }

        public override void Move()
        {
            MoveWithRb(_rb2d, TempMoveDirection, PlayerData.Speed, TimeScaleResistant);
        }

        public void Move(Vector2 direction, float speed)
        {
            MoveWithRb(_rb2d, direction, speed, TimeScaleResistant);
        }

        public override void Stand()
        {
            _rb2d.velocity = Vector2.zero;
            StateMachine.ChangeState(IdleState);
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
            DashState.Direction = (TempMousePosition - (Vector2)transform.position).normalized;
            StateMachine.ChangeState(DashState);
        }

        protected void OnFire(bool firing)
        {
            if (firing) AttackState.EnterState();
            else AttackState.ExitState();
        }

        protected void OnSwitchGun()
        {
            (CurrentGun, SecondGun) = (SecondGun, CurrentGun);
            OnPlayerGunSwitched?.Invoke(CurrentGun);
        }

        #endregion
    }
}
