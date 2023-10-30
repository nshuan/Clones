using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollection : MonoBehaviour
{
    public static readonly Gun pistol = new Gun("pistol", 0, 10, 0.8f, 0, 12f, 1f, 30f, 1, 0.08f);
    public static readonly Gun shotgun = new Gun("shotgun", 1, 10, 0.6f, 0, 10f, 1.2f, 30f, 6, 0.25f);
    public static readonly Gun smg = new Gun("smg", 2, 15, 3f, 0, 15f, 0.8f, 30f, 1, 0.08f, true);
    public static readonly Gun twinSmg = new Gun("twin smg", 3, 10, 3f, 0, 15f, 0.8f, 30f, 2, 0.08f, true);
    public static readonly Gun cherryCanon = new Gun("cherry canon", 4, 30, 0.3f, 1, 12f, 4f, 0.6f, 1, 0.08f);
    

    public static List<Gun> guns = new List<Gun>();
    public static BulletCollection bulletCollection;

    private void Awake()
    {
        guns.Add(pistol);
        guns.Add(shotgun);
        guns.Add(smg);
        guns.Add(twinSmg);
        guns.Add(cherryCanon);

        bulletCollection = GetComponent<BulletCollection>();
    }

    public static Gun GetGun(int id)
    {
        Gun returnGun = null;

        foreach (Gun gun in guns)
        {
            if (gun.GetId() == id)
            {
                returnGun = gun;
                break;
            }
        }
        
        return returnGun;
    }

    public static Gun GetRandomGun()
    {
        return guns[Random.Range(0, guns.Count)];
    }
}
