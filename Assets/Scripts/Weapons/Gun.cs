
using System;
using UnityEngine;

[Serializable]
public class Gun
{
    [SerializeField] private string name;
    [SerializeField] private GunType type;
    [SerializeField] private int id;
    [SerializeField] private int baseDamage;
    [SerializeField] private float fireRate;
    [SerializeField] private int bulletType;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletScale;
    [SerializeField] private float bulletLifeLength;
    [SerializeField] private int bulletsEachShot;
    [SerializeField] private float spread;
    [SerializeField] private bool automatic;
    
    public Gun(GunType type, int id, int baseDamage, float fireRate, int bulletType, float bulletSpeed, float bulletScale, float bulletLifeLength, int bulletEachShot, float spread)
    {
        this.type = type;
        this.id = id;
        this.baseDamage = baseDamage;
        this.fireRate = fireRate;
        this.bulletType = bulletType;
        this.bulletSpeed = bulletSpeed;
        this.bulletScale = bulletScale;
        this.bulletLifeLength = bulletLifeLength;
        this.bulletsEachShot = bulletEachShot;
        this.spread = spread;
        this.automatic = false;
    }

    public Gun(GunType type, int id, int baseDamage, float fireRate, int bulletType, float bulletSpeed, float bulletScale, float bulletLifeLength, int bulletEachShot, float spread, bool automatic)
    {
        this.type = type;
        this.id = id;
        this.baseDamage = baseDamage;
        this.fireRate = fireRate;
        this.bulletType = bulletType;
        this.bulletSpeed = bulletSpeed;
        this.bulletScale = bulletScale;
        this.bulletLifeLength = bulletLifeLength;
        this.bulletsEachShot = bulletEachShot;
        this.spread = spread;
        this.automatic = automatic;
    }

    public string GetName()
    {
        return type.ToString().ToLower();
    }
    
    public GunType GetGunType()
    {
        return type;
    }

    public int GetId()
    {
        return id;
    }

    public int GetBaseDamage()
    {
        return baseDamage;
    }
    
    public float GetFireRate()
    {
        return fireRate;
    }

    public int GetBulletType()
    {
        return bulletType;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public float GetBulletScale()
    {
        return bulletScale;
    }

    public float GetBulletLifeLength()
    {
        return bulletLifeLength;
    }

    public int GetBulletNum()
    {
        return bulletsEachShot;
    }

    public float GetSpread()
    {
        return spread;
    }

    public bool IsAutomatic()
    {
        return automatic;
    }
}
