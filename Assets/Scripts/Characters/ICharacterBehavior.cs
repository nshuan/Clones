using UnityEngine;

namespace Characters
{
    public interface ICharacterBehavior
    {
        void HitEnemy(Transform enemy);
        int HitPlayer();    // return damage
        void Damaged(int value);
        void Die();
        void Fire();

        void Aim(Vector3 targetPos);
        void Move(Vector3 target);
        void Stand(float duration);
        void Freeze();
        void Thaw();
        void Bounce(Vector2 direction, float length, float duration);
        void Dash(Vector2 direction, float length, float dashSp);
    }
}