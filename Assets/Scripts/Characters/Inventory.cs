using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    private int maxSlot = 2;

    // currentGunSlot = 0 means using primaryGun
    // else
    private int[] gunSlots;
    private int currentGunSlot = 0;


    void Awake()
    {
        gunSlots = new int[maxSlot];

        gunSlots[0] = Random.Range(0, 5);
        gunSlots[1] = gunSlots[0];
        while (gunSlots[1] == gunSlots[0])
        {
            gunSlots[1] = Random.Range(0, 5);
        }
    }

    public int GetMaxSlot()
    {
        return maxSlot;
    }

    public int GetCurrentGunSlot()
    {
        return currentGunSlot;
    }
    
    public int GetCurrentGunId()
    {
        return gunSlots[currentGunSlot % gunSlots.Length];
    }

    public int GetGunIdAt(int index)
    {
        return gunSlots[Mathf.Clamp(index, 0, gunSlots.Length - 1)];
    }

    public int SwitchGun(int slotIndex)
    {
        currentGunSlot = slotIndex;
        return gunSlots[currentGunSlot];
    }

    public int SwitchNextGun()
    {
        currentGunSlot = (currentGunSlot + 1) % gunSlots.Length;
        return gunSlots[currentGunSlot];
    }

    public void ReplaceGun(int slot, int newGunId)
    {
        gunSlots[slot] = Mathf.Clamp(newGunId, 0, GunManager.Instance.gunCount - 1);
    }
}
