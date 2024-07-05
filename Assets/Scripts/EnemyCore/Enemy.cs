using System;
using Character.Interfaces;
using EnemyCore.Behavior_Logic.Attack;
using EnemyCore.Behavior_Logic.Chase;
using EnemyCore.Behavior_Logic.Idle;
using EnemyCore.EnemyData;
using EnemyCore.State_Machine;
using UnityEngine;

namespace EnemyCore
{
    public class Enemy : PhysicalObjectBehavior, IDamageable, ITriggerCheckable
    {
        public Transform Target { get; private set; }
        
        #region State Machine

        public EnemyStateMachine StateMachine { get; protected set; }
        public EnemyIdleState IdleState { get; protected set; }
        public EnemyChaseState ChaseState { get; protected set; }
        public EnemyAttackState AttackState { get; protected set; }
        public EnemyBounceState BounceState { get; protected set; }

        #endregion

        #region Behavior

        [SerializeField] protected EnemyIdleSOBase idleBase;
        [SerializeField] protected EnemyChaseSOBase chaseBase;
        [SerializeField] protected EnemyAttackSOBase attackBase;
    
        public EnemyIdleSOBase IdleBase { get; protected set; }
        public EnemyChaseSOBase ChaseBase { get; protected set; }
        public EnemyAttackSOBase AttackBase { get; protected set; }
        
        #endregion

        #region Visual

        [SerializeField] private SpriteRenderer spriteRenderer;

        #endregion

        [field: SerializeField] public EnemyStats Stats { get; protected set; } = new EnemyStats();
        public int MaxHealth { get; protected set; }

        public int CurrentHealth { get; set; }
        
        public bool IsAggro { get; set; }
        public bool IsWithinStrikingDistance { get; set; }

        private void Awake()
        {
            Target = GameManager.Instance.player;

            // IdleBase = Instantiate(idleBase);
            ChaseBase = Instantiate(chaseBase);
            AttackBase = Instantiate(attackBase);
            
            StateMachine = new EnemyStateMachine();
            IdleState = new EnemyIdleState(this, StateMachine);
            ChaseState = new EnemyChaseState(this, StateMachine);
            AttackState = new EnemyAttackState(this, StateMachine);
            BounceState = new EnemyBounceState(this, StateMachine);
        }

        protected virtual void Start()
        {
            // IdleBase.Initialize(gameObject, this);
            ChaseBase.Initialize(gameObject, this);
            AttackBase.Initialize(gameObject, this);
            
            StateMachine.Initialize(ChaseState);
        }

        private void Update()
        {
            StateMachine.CurrentState.FrameUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        public void InitEnemy(int level)
        {
            MaxHealth = Stats.MaxHealth + 10 * level;
            CurrentHealth = MaxHealth;
            spriteRenderer.color = Stats.VisualColor;
            transform.localScale = Vector3.one * Stats.SizeScale;
        }

        public virtual void Damage(int value)
        {
            UIManager.Instance.CreateFloatText(transform, value, Color.cyan);
            CurrentHealth -= value;
            if (CurrentHealth < 0) Die();
        }

        public virtual void Die()
        {
            EnemyManager.Instance.EnemyDied(this);
            // DropItems();
            //
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

        public override void Move()
        {
            MoveWithoutRb(Target.position, Stats.Speed, Stats.TimeScaleResistant);
        }

        public void Move(Vector2 position, float speed)
        {
            MoveWithoutRb(position, speed, Stats.TimeScaleResistant);
        }

        #region Animation Triggers

        private void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            
        }

        #endregion

        #region Detection

        public void SetAggroStatus(bool isAggro)
        {
            IsAggro = isAggro;
        }

        public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
        {
            IsWithinStrikingDistance = isWithinStrikingDistance;
        }
        
        #endregion
    }
    
    public enum AnimationTriggerType
    {
            
    }
}