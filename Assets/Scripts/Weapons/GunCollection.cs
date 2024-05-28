using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Gun Collection", fileName = "GunCollection", order = 1)]
public class GunCollection : ScriptableObject
{
    public List<Gun> guns = new List<Gun>();
    
    [Button]
    private void InitGunId()
    {
        for (var index = 0; index < guns.Count; index++)
        {
            var gun = guns[index];
            gun.SetId(index);
        }
    }
}

public enum GunType
{
    Pistol,
    Shotgun,
    Smg,
    TwinSmg,
    CherryCanon,
    Cherry8,
    Cherry16
}
