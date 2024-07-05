using System;
using Core.ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyCore.Trigger_Checks
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyHitCheck : MonoBehaviour
    {
        public Transform Target { get; set; }
        private Enemy _enemy;

        [SerializeField] private LayerMask obstacleLayers;
        [SerializeField] private float hitRange = 1f;

        private void Awake()
        {
            Target = GameManager.Instance.player;
            _enemy = GetComponentInParent<Enemy>();
        }

        private void FixedUpdate()
        {
            CheckHit();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Equals(collision.gameObject, Target.gameObject))
            {
#if UNITY_EDITOR
                Debug.LogWarning("---- Enemy: " + name + " hit player!");
#endif
                var dmg = (int)(Target.localScale.magnitude / transform.localScale.magnitude * _enemy.Stats.BaseDamage);
                _enemy.Damage(dmg);
                _enemy.StateMachine.ChangeState(_enemy.BounceState);
            }
        }
        
        protected void CheckHit()
        {
            RaycastHit2D hit = Physics2D.CircleCast(
                _enemy.transform.position, 
                hitRange, 
                _enemy.transform.forward, 
                0.001f, 
                obstacleLayers);

            if (hit.collider is null) return;

            if (Equals(hit.collider.gameObject.layer, LayerMask.NameToLayer("PlayerBullet")))
            {
                if (hit.collider.gameObject.TryGetComponent<BulletBehavior>(out var bullet))
                {
#if UNITY_EDITOR
                    // Debug.Log("---- Enemy " + _enemy.gameObject.name + " hurt " + bullet.Damage);
#endif
                    _enemy.Damage(bullet.Damage);
                    bullet.BulletHit(transform);
                    PoolManager.Instance.Release(bullet);
                }
            }
        }

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, hitRange);
        }

        #endregion
    }

    public interface IRayCastIntrudedStrategy
    {
        void OnRayCastIntruded(RaycastHit2D hit);
    }

    public class RayCastEnemyIntruded : IRayCastIntrudedStrategy
    {
        public void OnRayCastIntruded(RaycastHit2D hit)
        {
            throw new NotImplementedException();
        }
    }
    
    public class RayCastBulletIntruded : IRayCastIntrudedStrategy
    {
        public void OnRayCastIntruded(RaycastHit2D hit)
        {
            throw new NotImplementedException();
        }
    }
}