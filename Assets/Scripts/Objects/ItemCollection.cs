using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();

    public GameObject GetItem(int id)
    {
        return items[Mathf.Min(id, items.Count - 1)];
    }
}
