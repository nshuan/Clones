using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollection : MonoBehaviour
{
    [SerializeField] private List<GameObject> bosses = new List<GameObject>();

    public GameObject GetBoss(int index)
    {
        return bosses[Mathf.Min(index, bosses.Count - 1)];
    }
}
