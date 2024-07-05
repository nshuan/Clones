using Core.ObjectPooling;
using EnemyCore.Behavior_Logic.Attack;
using Managers;
using UnityEngine;

namespace EnemyCore.Boss
{
    [CreateAssetMenu(fileName = "EnemyAttackTheDot1", menuName = "Scriptable Objects/Enemy Logic/Attack Logic/TheDotAttack1", order = 1)]
    public class EnemyAttackTheDot1 : EnemyAttackSOBase
    {
        private Gun _currentGun;
        private Gun _secondGun;

        private float _gunCd;
        private float _gunCdCounter;
        private BulletBehavior _bullet;
        private const float BulletOffset = 1.5f;
        private readonly Color _bulletColor = Color.red;
        private Vector2 _fireDirection;
        
        public override void Initialize(GameObject gameObject, Enemy enemy)
        {
            base.Initialize(gameObject, enemy);
            
            if (enemy.Stats is null) return;
            SetupGun();
        }

        public override void DoFrameUpdateLogic()
        {
            base.DoFrameUpdateLogic();

            if (EnemyRef.IsWithinStrikingDistance == false) EnemyRef.StateMachine.ChangeState(EnemyRef.ChaseState);
            
            if (_gunCdCounter < _gunCd)
            {
                _gunCdCounter += Time.deltaTime * GameManager.Instance.TimeScale + EnemyRef.Stats.TimeScaleResistant;
                return;
            }
            
            _fireDirection = (Target.position - EnemyRef.transform.position).normalized;
            Fire();
            _gunCdCounter = 0f;
            
        }

        private void SetupGun()
        {
            _currentGun = GunManager.Instance.GetGun(GunType.Cherry8);
            _secondGun = GunManager.Instance.GetGun(GunType.Cherry16);

            _gunCd = 1 / 1.6f / _currentGun.GetFireRate();
            // _gunCd = 2f;
            _gunCdCounter = 0f;
            _bullet = _currentGun.GetBullet();
        }
        
        protected void Fire()
        {
            if (_currentGun is null) return;
            if (_bullet is null) return;

            for (int i = 0; i < _currentGun.GetBulletNum(); i++)
            {
                // GameObject b = Instantiate(gunBullet, transform.position + transform.up * bulletOffset, transform.rotation);
                var bullet = PoolManager.Instance.Get(_bullet);
                bullet.transform.position = EnemyRef.transform.position + EnemyRef.transform.up * BulletOffset;
                bullet.transform.rotation = EnemyRef.transform.rotation;
                bullet.transform.localScale *= _currentGun.GetBulletScale();

                Vector2 originalDir = _fireDirection.normalized;
                Vector2 deflection = Vector2.Perpendicular(originalDir) * Random.Range(-_currentGun.GetSpread(), _currentGun.GetSpread());
                bullet.SetBulletStats(EnemyRef.Stats.BaseDamage + _currentGun.GetBaseDamage(), _currentGun.GetBulletSpeed(), _currentGun.GetBulletLifeLength(), originalDir + deflection, _bulletColor - new Color(0f, 0f, 0f, 0.5f), "EnemyBullet");
            }
        }
    }
}