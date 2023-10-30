using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HiveBehavior : MonoBehaviour
{
    [SerializeField] private Light2D hiveLight;

    private Transform player;
    private float maxIntensify = 3f;
    private float maxRange = 3f;

    void Start()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        if (player == null)
        {
            hiveLight.intensity = 0f;
            return;
        }

        if (Vector2.Distance(transform.position, player.position) > maxRange)
        {
            hiveLight.intensity = 0f;
            return;
        }

        hiveLight.intensity = maxIntensify * (1 - Vector2.Distance(transform.position, player.position) / maxRange);
    }
}
