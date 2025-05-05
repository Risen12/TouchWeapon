using UnityEngine;

public class PhysicWeaponLogic : WeaponLogic
{
    private float _powerOfShot;

    public void SetPower(float power) => _powerOfShot = power;

    public override void Shoot()
    {
        PhysicBullet bullet = BulletPool.Get() as PhysicBullet;
        bullet.SetPower(PowerOfShot);

        bullet.CollisionHappened += OnCollisionHappened;

        Vector2 direction = GetDirection();

        bullet.Launch(direction, WeaponData.BulletSpeed);
    }
}
