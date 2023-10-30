using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatText : MonoBehaviour
{
    private Transform target;
    public TMP_Text tmp_text;
    public float duration;
    private float counter = 0f;
    private Vector3 offset;
    private Vector3 tempOffset;
    private Vector3 floatVel;

    void Start()
    {
        offset = new Vector3(0f, 0.5f, 0f);
        tempOffset = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        if (counter < duration)
        {
            counter += Time.deltaTime;
            tmp_text.color -= new Color(0f, 0f, 0f, Time.deltaTime / duration);

            if (target == null) return;

            tempOffset = Vector3.SmoothDamp(tempOffset, offset, ref floatVel, 0.2f);
            transform.position = target.position + new Vector3(0f, 0.5f, 0f) + tempOffset;
        }
        else
        {
            Destroy(gameObject);   
        }
    }

    public void Setup(Transform target, float duration, int value, Color color)
    {
        this.target = target;
        this.duration = duration;
        tmp_text.text = value.ToString();
        tmp_text.color = color;
    }
}
