using System;
using UnityEngine;

public abstract class WeaponData : ScriptableObject
{
    [SerializeField] protected Sprite WeaponIcon;
    [SerializeField] protected Bullet BulletPrefab;
    [SerializeField] protected float Speed;

    public bool IsWeaponWithCharging;

    public Sprite WeaponImage => WeaponIcon;
    public float BulletSpeed => Speed;
    public Bullet PrefabOfBullet => BulletPrefab;
}