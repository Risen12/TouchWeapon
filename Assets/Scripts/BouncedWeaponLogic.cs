using UnityEngine;

public class BouncedWeaponLogic : WeaponLogic
{
    private float _topBorder;
    private float _bottomBorder;
    private float _leftBorder;
    private float _rightBorder;

    protected void Awake()
    {
        base.Awake();
        SetScreenSize();
    }

    public override void Shoot()
    {
        BouncedBullet bullet = BulletPool.Get() as BouncedBullet;

        bullet.CollisionHappened += OnCollisionHappened;
        bullet.Initialize(_leftBorder, _rightBorder, _topBorder, _bottomBorder);

        float angleRad = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;

        Vector2 bulletDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

        bullet.Launch(bulletDirection, WeaponData.BulletSpeed);
    } 

    private void SetScreenSize()
    {
        float divider = 2f;
        float heightMultiplyer = 2f;

        Camera camera = Camera.main;

        float height = camera.orthographicSize * heightMultiplyer;
        float width = height * Screen.width / Screen.height;

        Vector2 screenCenter = camera.transform.position;

        _leftBorder = screenCenter.x - width / divider;
        _rightBorder = screenCenter.x + width / divider;
        _bottomBorder = screenCenter.y - height / divider;
        _topBorder = screenCenter.y + height / divider;
    }
}
