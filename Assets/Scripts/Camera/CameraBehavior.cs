using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    private Vector3 velocity = Vector3.zero;

    private float defaultZoom = 8f;
    private float zoomDamping = 1f;
    private float velocityZoom;

    void Start()
    {
        CameraShake.ShakeOnce(1.5f, 3f);
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 movePos = target.position + offset;
        
        transform.position = Vector3.SmoothDamp(transform.position, movePos, ref velocity, damping);

        // Zoom
        float desiredZoom = Mathf.Min(10f, defaultZoom + (velocity.magnitude / 4)); //+ height;

        Camera.main.orthographicSize =
			Mathf.SmoothDamp(Camera.main.orthographicSize, desiredZoom, ref velocityZoom, zoomDamping);
		transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
