using UnityEngine;
using UnityEngine.Pool;

public abstract class WeaponLogic : MonoBehaviour, IWeaponLogic
{
    protected Transform BulletPlace;
    protected WeaponData WeaponData;
    protected float PowerOfShot;
    protected ObjectPool<Bullet> BulletPool;

    public bool IsWeaponWithCharging { get; protected set; }

    protected void Awake()
    {
        PowerOfShot = 0f;
        int bulletCount = 100;

        BulletPool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: GetBullet,
            actionOnRelease: ReleaseBullet,
            actionOnDestroy: (bullet) => Destroy(bullet),
            collectionCheck: false,
            defaultCapacity: bulletCount, 
            maxSize: bulletCount
            );
    }

    public abstract void Shoot();

    public void Initilaze(Transform bulletPace, WeaponData weaponData)
    {
        BulletPlace = bulletPace;
        WeaponData = weaponData;
        IsWeaponWithCharging = WeaponData.IsWeaponWithCharging;
    }

    protected void GetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.rotation = transform.rotation;
        bullet.transform.position = BulletPlace.position;
    }

    protected void ReleaseBullet(Bullet bullet)
    {
        bullet.CollisionHappened -= OnCollisionHappened;
        bullet.gameObject.SetActive(false);
    }

    protected Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(WeaponData.PrefabOfBullet, BulletPlace.position, Quaternion.identity);

        bullet.CollisionHappened += OnCollisionHappened;

        return bullet;
    }

    protected void OnCollisionHappened(Bullet bullet)
    {
        bullet.CollisionHappened -= OnCollisionHappened;
        BulletPool.Release(bullet);
    }

    protected Vector2 GetDirection()
    {
        float angleRad = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;

        Vector2 bulletDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

        return bulletDirection;
    }

    public void SetShootPower(float power)
    {
        PowerOfShot = power;
    }
}