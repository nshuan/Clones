using System;
using Scripts.PlayerSettings;
using UnityEngine;

namespace EnemyCore.Trigger_Checks
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyAggroCheck : MonoBehaviour
    {
        public GameObject Target { get; set; }
        private Enemy _enemy;

        private void Awake()
        {
            Target = GameManager.Instance.player.gameObject;
            _enemy = GetComponentInParent<Enemy>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Equals(collision.gameObject, Target))
            {
                _enemy.SetAggroStatus(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (Equals(collision.gameObject, Target))
            {
                _enemy.SetAggroStatus(false);
            }
        }
    }
}