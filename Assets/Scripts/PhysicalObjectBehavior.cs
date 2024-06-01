using UnityEngine;

public abstract class PhysicalObjectBehavior : MonoBehaviour
{
    public float Weight { get; protected set; }
    public float Velocity { get; protected set; } // Pixel per second
}