using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{
    public static FxManager Instance { get; set; }

    #region Serializable
    [Header("Enemy Destroyed Fx")]
    [SerializeField] private ParticleSystem enemyDestroyFx;
    private ParticleSystem.MainModule enemyDestroyM;
    #endregion

    void Awake()
    {
        Instance = this;

        enemyDestroyM = enemyDestroyFx.main;
    }

    public void CreateEnemyDestroyFx(Vector2 position, Color baseColor)
    {
        enemyDestroyM.startColor = new ParticleSystem.MinMaxGradient(baseColor - new Color(0f, 0f, 0f, 0.75f), baseColor - new Color(0f, 0f, 0f, 0.25f));
        Instantiate(enemyDestroyFx, position, Quaternion.identity).Play();
    }
}
