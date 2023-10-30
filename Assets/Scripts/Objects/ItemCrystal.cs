using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemCrystal : MonoBehaviour
{
    public ItemData itemData = new ItemData();

    private int health = 30;

    #region Temporary variables
    private Vector2 tempDirection;
    private Vector2 tempDestination;
    private Vector2 tempVel;
    #endregion

    public abstract void BreakCrystal();

    void Start()
    {
        tempDirection = 2 * Random.insideUnitCircle;
        tempDestination = (Vector2) transform.position + tempDirection;
    }

    void Update()
    {
        if (health <= 0)
        {
            if (!UIManager.Instance.replacingGun)
                BreakCrystal();
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector2.SmoothDamp(transform.position, tempDestination, ref tempVel, 0.2f);
    }

    public void SetValues(string name, int id, int value)
    {
        itemData.itemName = name;
        itemData.id = id;
        itemData.value = value;
    }

    public void Damaged()
    {
        health -= 10;
    }
}

public class ItemData
{
    public string itemName;
    public int id;

    // Value of health is the amount of health to heal
    // Value of gun is the gun id
    public int value;

    public ItemData()
    {
        this.itemName = "ItemCrystal";
        this.id = 0;
        this.value = 0;
    }

    public ItemData(string name, int id, int value)
    {
        this.itemName = name;
        this.id = id;
        this.value = value;
    }
}
