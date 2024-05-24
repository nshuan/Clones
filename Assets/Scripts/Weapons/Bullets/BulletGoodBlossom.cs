using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGoodBlossom : BulletBehavior
{
    public int childBulletType;
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
            Destroy(gameObject);
    }

    private void Blossom()
    {
        GameObject childBullet = BulletCollection.Instance.GetBullet(childBulletType);
        for (int i = 0; i < childNum; i++)
        {
            GameObject b = Instantiate(childBullet, transform.position, transform.rotation);
            BulletBehavior bScript = b.GetComponent<BulletBehavior>();

            Vector2 originalDir = direction.normalized;
            float angle = (Mathf.Atan2(originalDir.y, originalDir.x) - i * 360 / childNum) * Mathf.Deg2Rad;
            originalDir.x = Mathf.Cos(angle);
            originalDir.y = Mathf.Sin(angle);
            
            bScript.SetBulletStats(Mathf.Max(10, Mathf.FloorToInt(damage / childNum)), childSpeed + waveCount * 0.125f, 30f, originalDir, spriteRenderer.color, LayerMask.LayerToName(gameObject.layer));
        }
    }
}
