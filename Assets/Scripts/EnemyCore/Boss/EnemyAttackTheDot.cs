using Characters.Bosses;
using Core.ObjectPooling;
using EnemyCore.Behavior_Logic.Attack;
using Managers;
using UnityEngine;

namespace EnemyCore.Boss
{
    [CreateAssetMenu(fileName = "EnemyAttackTheDot", menuName = "Scriptable Objects/Enemy Logic/Attack Logic/TheDotAttack", order = 1)]
    public class EnemyAttackTheDot : EnemyAttackSOBase
    {
        private Gun _currentGun;
        private Gun _secondGun;

        private float _gunCd;
        private float _gunCdCounter;
        private BulletBehavior _bullet;
        private const float BulletOffset = 1.5f;
        private readonly Color _bulletColor = Color.red;
        private Vector2 _fireDirection;

        private int _maxDrill = 3;
        private int _drillTurn = 0;
        private float _drillCd = 3f;
        private float _drillCdCounter = 0f;
        private float _drillWait = 8f;
        private float _drillWaitCounter = 0f;
        private Vector2 _originalPos;
        private bool _drilling = false;
        private float DrillSpeed => 12 * EnemyRef.Stats.Speed;
        
        public override void Initialize(GameObject gameObject, Enemy enemy)
        {
            base.Initialize(gameObject, enemy);
            
            if (enemy.Stats is null) return;
            SetupGun();
        }

        public override void DoFrameUpdateLogic()
        {
            base.DoFrameUpdateLogic();

            if (!_drilling)
            {
                if (_gunCdCounter < _gunCd)
                {
                    _gunCdCounter += Time.deltaTime * GameManager.Instance.TimeScale + EnemyRef.Stats.TimeScaleResistant;
                }
                
                if (_drillWaitCounter < _drillWait)
                {
                    _drillWaitCounter += Time.timeScale * GameManager.Instance.TimeScale + EnemyRef.Stats.TimeScaleResistant;
                }
                else
                {
                    _drilling = true;
                    _drillWaitCounter = 0f;
                }
            }
            else
            {
                _drillCdCounter += Time.timeScale * GameManager.Instance.TimeScale + EnemyRef.Stats.TimeScaleResistant;
            }
        }

        public override void DoPhysicsUpdateLogic()
        {
            base.DoPhysicsUpdateLogic();

            if (_drilling)
            {
                if (_drillCdCounter < _drillCd) return;

                if (_drillTurn >= _maxDrill)
                {
                    EnemyRef.Move(_originalPos, DrillSpeed);
                    _drilling = false;
                }
                else
                {
                    EnemyRef.Move(Target.position, DrillSpeed);
                    _drillTurn += 1;
                }
                _drillCdCounter = 0f;
                
                return;
            }
            
            if (_gunCdCounter >= _gunCd)
            {
                _fireDirection = (Target.position - EnemyRef.transform.position).normalized;
                Fire();
                _gunCdCounter = 0f;
            }
        }

        private void SetupGun()
        {
            _currentGun = GunManager.Instance.GetGun(GunType.Cherry8);
            _secondGun = GunManager.Instance.GetGun(GunType.Cherry16);

            // _gunCd = 1 / 1.6f / _currentGun.GetFireRate();
            _gunCd = 2f;
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