using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; set; }

    public Camera mainCam;
    private CameraBehavior camScript;

    void Awake()
    {
        Instance = this;

        camScript = mainCam.GetComponent<CameraBehavior>();
    }

    public void ChangeTarget(Transform newTarget)
    {
        camScript.target = newTarget;
    }
}
