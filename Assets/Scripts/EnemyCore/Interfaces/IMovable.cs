using UnityEngine;

namespace Character.Interfaces
{
    public interface IMovable
    {
        void Aim(Vector3 target);
        void MoveWithoutRb(Vector3 target, float speed, float timeScaleResistant);
        void MoveWithRb(Rigidbody2D rb2d, Vector2 direction, float speed, float timeScaleResistant);
    }
}