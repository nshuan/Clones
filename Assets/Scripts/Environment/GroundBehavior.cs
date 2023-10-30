using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBehavior : MonoBehaviour
{
    private Transform target;

    public List<Transform> groundRow0 = new List<Transform>();
    public List<Transform> groundRow1 = new List<Transform>();
    public List<Transform> groundRow2 = new List<Transform>();
    public List<List<Transform>> groundMap = new List<List<Transform>>();

    void Start()
    {
        target = CameraManager.Instance.mainCam.transform;

        groundMap.Add(groundRow0);
        groundMap.Add(groundRow1);
        groundMap.Add(groundRow2);
    }

    void Update()
    {
        if (target == null) return;
        
        if (target.position.y < groundMap[1][1].position.y - 9f)
        {
            groundMap.Insert(0, groundMap[2]);
            groundMap.RemoveAt(3);

            foreach (Transform ground in groundMap[0])
            {
                ground.position += new Vector3(0f, -54f, 0f);
            }
        }
        if (target.position.y > groundMap[1][1].position.y + 9f)
        {
            groundMap.Add(groundMap[0]);
            groundMap.RemoveAt(0);

            foreach (Transform ground in groundMap[2])
            {
                ground.position += new Vector3(0f, 54f, 0f);
            }
        }
        if (target.position.x < groundMap[1][1].position.x - 15f)
        {
            groundMap[0].Insert(0, groundMap[0][2]);
            groundMap[1].Insert(0, groundMap[1][2]);
            groundMap[2].Insert(0, groundMap[2][2]);
            groundMap[0].RemoveAt(3);
            groundMap[1].RemoveAt(3);
            groundMap[2].RemoveAt(3);

            foreach (List<Transform> groundRow in groundMap)
            {
                groundRow[0].position += new Vector3(-90f, 0f, 0f);
            }
        }
        if (target.position.x > groundMap[1][1].position.x + 15f)
        {
            groundMap[0].Add(groundMap[0][0]);
            groundMap[1].Add(groundMap[1][0]);
            groundMap[2].Add(groundMap[2][0]);
            groundMap[0].RemoveAt(0);
            groundMap[1].RemoveAt(0);
            groundMap[2].RemoveAt(0);

            foreach (List<Transform> groundRow in groundMap)
            {
                groundRow[2].position += new Vector3(90f, 0f, 0f);
            }
        }
    }
}
