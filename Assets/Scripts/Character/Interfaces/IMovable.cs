using UnityEngine;

namespace Character.Interfaces
{
    public interface IMovable
    {
        void Aim(Vector3 target);
        void Move(Vector3 target);
        void MoveRb(Vector2 direction);
    }
}