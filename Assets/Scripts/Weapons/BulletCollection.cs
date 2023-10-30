using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollection : MonoBehaviour
{
    [SerializeField] private List<GameObject> bullets = new List<GameObject>();

    public GameObject GetBullet(int index)
    {
        return bullets[Mathf.Min(index, bullets.Count - 1)];
    }
}
