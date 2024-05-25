using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class BulletCollection : MonoSingleton<BulletCollection>
{
    [SerializeField] private List<BulletBehavior> bullet = new List<BulletBehavior>();

    public BulletBehavior GetBullet(int index)
    {
        return bullet[Mathf.Min(index, bullet.Count - 1)];
    }
}
