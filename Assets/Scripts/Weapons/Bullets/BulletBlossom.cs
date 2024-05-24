using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBlossom : BulletBehavior
{
    public int childBulletType;

    public override void BulletHit()
    {
        Blossom();
        Destroy(gameObject);
    }

    private void Blossom()
    {
        // Number of bullets as children
        int childNum = Random.Range(8, 17);
        float childSpeed = 22f - childNum;

        GameObject childBullet = BulletCollection.Instance.GetBullet(childBulletType);
        for (int i = 0; i < childNum; i++)
        {
            GameObject b = Instantiate(childBullet, transform.position, transform.rotation);
            BulletBehavior bScript = b.GetComponent<BulletBehavior>();

            Vector2 originalDir = transform.up;
            float angle = (Mathf.Atan2(originalDir.y, originalDir.x) - i * 360 / childNum) * Mathf.Deg2Rad;
            originalDir.x = Mathf.Cos(angle);
            originalDir.y = Mathf.Sin(angle);
            
            bScript.SetBulletStats(Mathf.Max(10, Mathf.FloorToInt(damage / childNum)), childSpeed, 30f, originalDir.normalized, spriteRenderer.color, LayerMask.LayerToName(gameObject.layer));
        }
    }
}
