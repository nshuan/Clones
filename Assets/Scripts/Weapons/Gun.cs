
public class Gun
{
    // fire per second
    private string name;
    private int id;
    private int baseDamage;
    private float fireRate;
    private int bulletType;
    private float bulletSpeed;
    private float bulletScale;
    private float bulletLifeLength;
    private int bulletsEachShot;
    private float spread;
    private bool automatic;
    
    public Gun(string name, int id, int baseDamage, float fireRate, int bulletType, float bulletSpeed, float bulletScale, float bulletLifeLength, int bulletEachShot, float spread)
    {
        this.name = name;
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

    public Gun(string name, int id, int baseDamage, float fireRate, int bulletType, float bulletSpeed, float bulletScale, float bulletLifeLength, int bulletEachShot, float spread, bool automatic)
    {
        this.name = name;
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
        return name;
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
