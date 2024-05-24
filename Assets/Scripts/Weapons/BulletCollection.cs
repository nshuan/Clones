using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class BulletCollection : MonoSingleton<BulletCollection>
{
    [SerializeField] private List<GameObject> bullets = new List<GameObject>();

    public GameObject GetBullet(int index)
    {
        return bullets[Mathf.Min(index, bullets.Count - 1)];
    }
}
