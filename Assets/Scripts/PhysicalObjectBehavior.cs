using Character.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class PhysicalObjectBehavior : MonoBehaviour, IMovable
{
    public float Weight { get; protected set; }
    public virtual Vector2 Velocity { get; protected set; }// Pixel per second
    
    
    public void Aim(Vector3 target)
    {
        Vector2 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void MoveWithoutRb(Vector3 target, float speed, float timeScaleResistant)
    {
        var velMagnitude = 0.02f * speed * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f);
        Velocity = velMagnitude * Vector2.one;
            
        transform.position = Vector2.MoveTowards(transform.position, target, velMagnitude);
    }
    
    public void MoveWithRb(Rigidbody2D rb2d, Vector2 direction, float speed, float timeScaleResistant)
    {
        // If the character is moving diagonally, speed is multiply by 1.35f
        float speedScale = 1f;
        if (Mathf.Abs(direction.x) > 0.5f && Mathf.Abs(direction.y) > 0.5f)
        {
            speedScale = 1.35f;
        }

        rb2d.velocity = direction.normalized * (speed * speedScale * Mathf.Clamp(GameManager.Instance.TimeScale + timeScaleResistant, 0f, 1f));
    }

    public abstract void Move();
}
