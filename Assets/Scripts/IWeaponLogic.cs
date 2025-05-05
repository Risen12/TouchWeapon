using UnityEngine;

public interface IWeaponLogic
{
    public void Shoot();

    public void Initilaze(Transform bulletPace, WeaponData weaponData);

    public void SetShootPower(float power);
}