using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : ItemCrystal
{
    public override void BreakCrystal()
    {
        GameManager.Instance.HealPlayer(itemData.value);

        Destroy(gameObject);
    }
}
