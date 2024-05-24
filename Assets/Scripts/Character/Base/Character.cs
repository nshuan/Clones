using System;
using Character.Interfaces;
using Character.State_Machine;
using Character.State_Machine.States;
using UnityEngine;

namespace Character.Base
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour, IDamageable, IMovable
    {
        #region State Machine Variables

        public CharacterStateMachine StateMachine { get; set; }
        public CharacterMoveState MoveState { get; set; }
        public CharacterDashState DashState { get; set; }
        public CharacterAttackState AttackState { get; set; }

        #endregion
        
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public float Speed { get; set; } = 10f;
        public float TimeScaleResistant { get; set; } = 0f;
        
        protected Rigidbody2D Rb2d;


        protected virtual void Awake()
        {
            Rb2d = GetComponent<Rigidbody2D>();
            
            StateMachine = new CharacterStateMachine();
            
            MoveState = new CharacterMoveState(this, StateMachine);
            DashState = new CharacterDashState(this, StateMachine);
            AttackState = new CharacterAttackState(this, StateMachine);
        }

        protected void Start()
        {
            CurrentHealth = MaxHealth;
            
            StateMachine.Initialize(MoveState);
        }

        protected void Update()
        {
            StateMachine.CurrentState.FrameUpdate();
        }

        protected void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        #region Damage/Die functions
        public void Damage(int value)
        {
            CurrentHealth -= value;
            if (CurrentHealth < 0)
            {
                Die();
                return;
            }
        }

        public void Die()
        {
            // DropItems();

            // dieFx.transform.SetParent(null);
            // dieFx.transform.position = transform.position;
            // dieFx.Play();
            //
            // coinFx.transform.SetParent(null);
            // coinFx.transform.position = transform.position;
            // coinFx.Play();
            //
            // soulFx.transform.SetParent(null);
            // soulFx.transform.position = transform.position;
            // soulFx.Play();

            Destroy(gameObject);
        }
        #endregion

        #region Movement
        public void Aim(Vector3 target)
        {
            Vector2 direction = target - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void Move(Vector3 target)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                target, 0.02f * Speed * Mathf.Clamp(GameManager.Instance.TimeScale + TimeScaleResistant, 0f, 1f));
        }

        public void MoveRb(Vector2 direction)
        {
            // If the character is moving diagonally, speed is multiply by 1.35f
            float speedScale = 1f;
            if (Mathf.Abs(direction.x) > 0.5f && Mathf.Abs(direction.y) > 0.5f)
            {
                speedScale = 1.35f;
            }

            Rb2d.velocity = direction.normalized * (Speed * speedScale * Mathf.Clamp(GameManager.Instance.TimeScale + TimeScaleResistant, 0f, 1f));
        }

        protected void Dash(Vector2 target, float length, float dashSp) 
        {
            DashState.SetupTemp(target, length, dashSp);
        }
        #endregion

        #region Animation Triggers

        private void AnimationTriggerEvent(CharacterTriggerType triggerType)
        {
            StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
        }

        #endregion
    }

    public enum CharacterTriggerType
    {
        
    }
}