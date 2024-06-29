using System.Collections;
using Core.ObjectPooling;
using UnityEngine;

namespace PlayerCore.State_Machine
{
    public class PlayerAttackState : PlayerState
    {
        private float _bulletOffset;
        private Color _bulletColor;
        private float _gunCooldown;
        private float _gunCdCounter;
        private bool _firing = false;
        
        public PlayerAttackState(PlayerBehavior player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            
            _bulletOffset = 1.5f;
            // _bulletColor = Player.color;
            _bulletColor = Color.cyan;
            
            _gunCooldown = 1 / 1.6f / Player.CurrentGun.GetFireRate();
            _firing = true;
        }

        public override void ExitState()
        {
            base.ExitState();

            _firing = false;
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            
            if (_gunCdCounter < _gunCooldown)
                _gunCdCounter += Time.deltaTime * Mathf.Clamp(GameManager.Instance.TimeScale + Player.TimeScaleResistant, 0f, 1f);
            else
            {
                if (_firing)
                {
                    var fireDirection = (Player.TempMousePosition - (Vector2)Player.transform.position).normalized;
                    Fire(Player.CurrentGun, fireDirection);
                    SoundManager.Instance.PlayFireSound();
                    _gunCdCounter = 0;
                    
                    if (Player.CurrentGun.IsAutomatic() == false)
                        _firing = false;
                }
            }
        }
        
        public void Fire(Gun gun, Vector2 direction)
        {
            for (int i = 0; i < gun.GetBulletNum(); i++)
            {
                // GameObject b = Instantiate(gunBullet, transform.position + transform.up * bulletOffset, transform.rotation);
                var bullet = PoolManager.Instance.Get(gun.GetBullet());
                bullet.transform.position = Player.transform.position + Player.transform.up * _bulletOffset;
                bullet.transform.rotation = Player.transform.rotation;
                bullet.transform.localScale *= gun.GetBulletScale();
                
                Vector2 deflection = Vector2.Perpendicular(direction) * Random.Range(-gun.GetSpread(), gun.GetSpread());
            
                bullet.SetBulletStats(Player.BaseDamage + gun.GetBaseDamage(), gun.GetBulletSpeed(), gun.GetBulletLifeLength(), direction + deflection, _bulletColor - new Color(0f, 0f, 0f, 0.5f), "PlayerBullet");
            }
        }
    }
}