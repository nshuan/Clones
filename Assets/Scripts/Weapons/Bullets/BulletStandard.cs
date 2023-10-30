using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStandard : BulletBehavior
{
    public override void BulletHit()
    {
        // destroyFx.transform.SetParent(null);
        // destroyFx.Play();
        Destroy(gameObject);
    }
}
