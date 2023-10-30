using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static Transform camTransform;
	
    private static float shakeDuration = 0f;

    private static float shakeAmount = 0.7f;

    private static bool shake = false;

    private float vel;
    private Vector3 vel2 = Vector3.zero;
	
    private static Vector3 originalPos;
	
    void Awake() {
        if (camTransform == null) {
            camTransform = transform;
        }
    }

    void Update() 
    {
        if (!shake) return;

        if (shakeDuration > 0) {
            Vector3 newPos = originalPos + Random.insideUnitSphere * shakeAmount;

            camTransform.localPosition = Vector3.SmoothDamp(camTransform.localPosition, newPos, ref vel2, 0.05f);

            shakeDuration -= Time.deltaTime;
            shakeAmount = Mathf.SmoothDamp(shakeAmount, 0, ref vel, 0.7f);
        }
        else {
            camTransform.localPosition = originalPos;
            shake = false;
        }
		
    }

    public static void ShakeOnce(float lenght, float strength) {
        originalPos = camTransform.localPosition;
        shakeDuration = lenght;
        shakeAmount = strength;
        shake = true;
    }
}
