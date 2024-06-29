using System.Collections;
using System.Collections.Generic;
using Core.ObjectPooling;
using UnityEngine;

public class BulletStandard : BulletBehavior
{
    [SerializeField] protected TrailRenderer trail;
    
    public override void BulletHit(Transform obstacle)
    {
        // destroyFx.transform.SetParent(null);
        // destroyFx.Play();
        DestroyBullet();
    }

    protected override void SetupTrail(Color color)
    {
        if (trail != null)
        {
            trail.startColor = color - new Color(0f, 0f, 0f, color.a / 3);
            trail.endColor = color - new Color(0f, 0f, 0f, 1f);
        }
    }

    protected override void DestroyBullet()
    {
        trail.Clear();
        base.DestroyBullet();
    }
}
