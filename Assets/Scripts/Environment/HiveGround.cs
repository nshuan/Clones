using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveGround : MonoBehaviour
{
    [SerializeField] private GameObject hivePrefab;

    void Start()
    {
        // n rows, n must not be divided by 2
        int n = 103;

        Vector2 anchorPos = new Vector2(-22, -38.25f);

        int i, j;
        for (i = 0; i < (n - 1) / 2; i++)
        {
            for (j = 0; j < (n + 1) / 2 + i; j++)
            {
                Instantiate(hivePrefab, anchorPos + new Vector2(i % 2 * -Mathf.Sqrt(3) / 4 + Mathf.Sqrt(3) / 2 * (j - i / 2), i * 0.75f), Quaternion.identity, transform);
                Instantiate(hivePrefab, anchorPos + new Vector2(0f, (n - 1) * 0.75f) + new Vector2(i % 2 * -Mathf.Sqrt(3) / 4 + Mathf.Sqrt(3) / 2 * (j - i / 2), -i * 0.75f), Quaternion.identity, transform);
            }
        }
        for (j = 0; j < (n + 1) / 2 + i; j++)
        {
            Instantiate(hivePrefab, anchorPos + new Vector2(i % 2 * -Mathf.Sqrt(3) / 4 + Mathf.Sqrt(3) / 2 * (j - i / 2), i * 0.75f), Quaternion.identity, transform);
        }
    }
}
