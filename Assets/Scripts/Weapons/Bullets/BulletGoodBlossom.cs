using System.Collections;
using System.Collections.Generic;
using Core.ObjectPooling;
using UnityEngine;

public class BulletGoodBlossom : BulletBehavior
{
    public BulletBehavior childBullet;
    [SerializeField] private float childScale;
    [SerializeField] private int childNum = 8;
    [SerializeField] private float childSpeed = 16f;
    [SerializeField] private int maxWave = 8;

    private int waveCount = 0;

    public override void BulletHit()
    {
        Blossom();
        lifeCount = 0f;
        waveCount += 1;
        direction += Vector2.Perpendicular(direction) * 0.5f;

        if (waveCount >= maxWave)
            DestroyBullet();
    }

    private void Blossom()
    {
        for (var i = 0; i < childNum; i++)
        {
            var b = PoolManager.Instance.Get<BulletBehavior>(childBullet);
            b.transform.position = transform.position;
            b.transform.rotation = transform.rotation;
            b.transform.localScale *= childScale;

            var originalDir = direction.normalized;
            var angle = (Mathf.Atan2(originalDir.y, originalDir.x) - i * 360 / childNum) * Mathf.Deg2Rad;
            originalDir.x = Mathf.Cos(angle);
            originalDir.y = Mathf.Sin(angle);
            
            b.SetBulletStats(Mathf.Max(10, Mathf.FloorToInt(damage / childNum)), childSpeed + waveCount * 0.125f, 30f, originalDir, spriteRenderer.color, LayerMask.LayerToName(gameObject.layer));
        }
    }
}
