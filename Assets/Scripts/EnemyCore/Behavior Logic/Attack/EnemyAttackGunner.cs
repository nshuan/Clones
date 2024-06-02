using Core.ObjectPooling;
using Managers;
using UnityEngine;

namespace EnemyCore.Behavior_Logic.Attack
{
    [CreateAssetMenu(fileName = "EnemyAttackGunner", menuName = "Scriptable Objects/Enemy Logic/Attack Logic/GunnerAttack", order = 1)]
    public class EnemyAttackGunner : EnemyAttackSOBase
    {
        private Gun _gun;
        private BulletBehavior _bullet;
        private float _gunCd;
        private float _gunCdCounter = 0f;
        private Vector2 _fireDirection;
        
        protected float TimeScaleResistant = 1f;
        protected float BulletOffset = 1.5f;
        protected Color BulletColor = Color.red;
        
        public override void Initialize(GameObject gameObject, Enemy enemy)
        {
            base.Initialize(gameObject, enemy);
            
            if (enemy.Stats is null) return;
            SetupGun(GunManager.Instance.GetGun(enemy.Stats.GunType));
        }

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
        }

        public override void DoFrameUpdateLogic()
        {
            base.DoFrameUpdateLogic();
            
            if (_gunCdCounter < _gunCd)
            {
                _gunCdCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + TimeScaleResistant, 0f, 1f);
            }
            else
            {
                _fireDirection =  Target.position - EnemyRef.transform.position;
                Fire();
                _gunCdCounter = 0f;
            }
            
            if (EnemyRef.IsWithinStrikingDistance == false) EnemyRef.StateMachine.ChangeState(EnemyRef.ChaseState);
        }

        public override void DoPhysicsUpdateLogic()
        {
            base.DoPhysicsUpdateLogic();
        }

        public override void DoAnimationTriggerEventLogic(AnimationTriggerType triggerType)
        {
            base.DoAnimationTriggerEventLogic(triggerType);
        }

        public override void ResetValue()
        {
            base.ResetValue();
        }
        
        protected void Fire()
        {
            if (_gun is null) return;
            if (_bullet is null) return;

            for (int i = 0; i < _gun.GetBulletNum(); i++)
            {
                // GameObject b = Instantiate(gunBullet, transform.position + transform.up * bulletOffset, transform.rotation);
                var bullet = PoolManager.Instance.Get(_bullet);
                bullet.transform.position = EnemyRef.transform.position + EnemyRef.transform.up * BulletOffset;
                bullet.transform.rotation = EnemyRef.transform.rotation;
                bullet.transform.localScale *= _gun.GetBulletScale();

                Vector2 originalDir = _fireDirection.normalized;
                Vector2 deflection = Vector2.Perpendicular(originalDir) * Random.Range(-_gun.GetSpread(), _gun.GetSpread());
                bullet.SetBulletStats(EnemyRef.Stats.BaseDamage + _gun.GetBaseDamage(), _gun.GetBulletSpeed(), _gun.GetBulletLifeLength(), originalDir + deflection, BulletColor - new Color(0f, 0f, 0f, 0.5f), "EnemyBullet");
            }
        }
        
        public void SetupGun(Gun gun)
        {
            _gun = gun;
            _gunCd = 1 / 1.6f / gun.GetFireRate();
            _bullet = gun.GetBullet();
        }
    }
}