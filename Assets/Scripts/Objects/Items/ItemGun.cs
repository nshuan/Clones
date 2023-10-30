using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGun : ItemCrystal
{   
    public override void BreakCrystal()
    {
        GameManager.Instance.FreezeGame();
        UIManager.Instance.ReplaceGun(itemData.value);

        Destroy(gameObject);
    }
}
