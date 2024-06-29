using System.Collections;
using System.Collections.Generic;
using Core.ObjectPooling;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletBlossom : BulletBehavior
{
    [SerializeField] private BulletBehavior childBullet;
    [SerializeField] private float childScale;
    
    public override void BulletHit(Transform obstacle)
    {
        Blossom();
        DestroyBullet();
    }

    private void Blossom()
    {
        // Number of bullets as children
        var childNum = Random.Range(8, 17);
        var childSpeed = 22f - childNum;
        
        for (var i = 0; i < childNum; i++)
        {
            var b = PoolManager.Instance.Get<BulletBehavior>(childBullet);
            b.transform.position = transform.position;
            b.transform.rotation = transform.rotation;
            b.transform.localScale *= childScale;

            Vector2 originalDir = transform.up;
            var angle = (Mathf.Atan2(originalDir.y, originalDir.x) - i * 360 / childNum) * Mathf.Deg2Rad;
            originalDir.x = Mathf.Cos(angle);
            originalDir.y = Mathf.Sin(angle);
            
            b.SetBulletStats(Mathf.Max(10, Mathf.FloorToInt(Damage / childNum)), childSpeed, 30f, originalDir.normalized, spriteRenderer.color, LayerMask.LayerToName(gameObject.layer));
        }
    }
}
