using UnityEngine;

namespace EnemyCore.Trigger_Checks
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyStrikingDistanceCheck : MonoBehaviour
    {
        private GameObject Target { get; set; }
        private Enemy _enemy;

        [SerializeField] private CircleCollider2D cld;
        
        private void Awake()
        {
            Target = GameManager.Instance.player.gameObject;
            _enemy = GetComponentInParent<Enemy>();
            
            // Setup range
            cld.radius = _enemy.Stats.AttackRange;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Equals(collision.gameObject, Target))
            {
                _enemy.SetStrikingDistanceBool(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (Equals(collision.gameObject, Target))
            {
                _enemy.SetStrikingDistanceBool(false);
            }
        }
    }
}