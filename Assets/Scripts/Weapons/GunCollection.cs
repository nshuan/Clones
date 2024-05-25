using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Gun Collection", fileName = "GunCollection", order = 1)]
public class GunCollection : ScriptableObject
{
    public List<Gun> guns = new List<Gun>();
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
