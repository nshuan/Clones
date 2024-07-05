using Core.ObjectPooling;
using UnityEngine;

namespace PlayerCore.Trigger_Checks
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerHitCheck : MonoBehaviour
    {
        public PlayerBehavior Player { get; set; }

        [SerializeField] private LayerMask obstacleLayers;
        [SerializeField] private float hitRange = 1f;

        private void Awake()
        {
            Player = GetComponentInParent<PlayerBehavior>();
        }

        private void FixedUpdate()
        {
            CheckHit();
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Equals(collision.gameObject.tag, "Enemy"))
            {
#if UNITY_EDITOR
                Debug.LogWarning("---- Player hit enemy: " + collision.gameObject.name);
#endif
                var enemy = collision.gameObject.transform;
                var dmg = (int)(enemy.localScale.magnitude / transform.localScale.magnitude * Player.BaseDamage);
                Player.TempHitObject = enemy;
                Player.Damage(dmg);
                // Player.BounceState.EnterState();
            }
        }
        
        protected void CheckHit()
        {
            RaycastHit2D hit = Physics2D.CircleCast(
                Player.transform.position, 
                hitRange, 
                Player.transform.forward, 
                0.001f, 
                obstacleLayers);

            if (hit.collider is null) return;

            if (Equals(hit.collider.gameObject.layer, LayerMask.NameToLayer("EnemyBullet")))
            {
                if (hit.collider.gameObject.TryGetComponent<BulletBehavior>(out var bullet))
                {
#if UNITY_EDITOR
                    // Debug.Log("---- Player hurt " + bullet.Damage);
#endif
                    Player.Damage(bullet.Damage);
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
}