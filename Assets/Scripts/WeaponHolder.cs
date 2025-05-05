using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponHolder : MonoBehaviour 
{
    [SerializeField] private List<WeaponData> _weaponTypes;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _minAngle;
    [SerializeField] private Transform _bulletSpawnPlace;
    [SerializeField] private WeaponCharger _weaponCharger;
    [SerializeField] private WeaponChanger _weaponChanger;

    private IWeaponLogic _currentWeapon;
    private WeaponData _currentWeaponData;
    private SpriteRenderer _spriteRenderer;

    public event Action Shooted;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_weaponTypes.Count > 0)
            ChangeWeapon(_weaponTypes[0]);

        _weaponChanger.Initialize(_weaponTypes.Count);
    }

    private void OnEnable()
    {
        _weaponChanger.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _weaponChanger.WeaponChanged -= OnWeaponChanged;
    }

    public void Rotate(float angle)
    { 
        if(angle > _maxAngle)
            angle = _maxAngle;

        if(angle < _minAngle)
            angle = _minAngle;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ChangeWeapon(WeaponData weaponData)
    {
        if (_currentWeapon != null)
            _currentWeapon = null;

        _currentWeaponData = weaponData;

        switch (weaponData) 
        {
            case PhysicWeaponData:
                _currentWeapon = gameObject.AddComponent<PhysicWeaponLogic>();
                break;

            case BouncedWeaponData:
                _currentWeapon = gameObject.AddComponent<BouncedWeaponLogic>();
                break;

            default:
                Debug.Log($"Can't define weaponLogic for {weaponData.GetType()}!");
                break;
        }

        _spriteRenderer.sprite = weaponData.WeaponImage;
        _currentWeapon.Initilaze(_bulletSpawnPlace, weaponData);
    }

    public void MouseButtonDown()
    {
        if (_currentWeaponData.IsWeaponWithCharging)
        {
            _weaponCharger.BeginCharge();
        }
        else
        {
            _currentWeapon.Shoot();
            Shooted?.Invoke();
        }
    }

    public void MouseButtonUp()
    {
        if (_currentWeaponData.IsWeaponWithCharging)
        {
            float shootPower = _weaponCharger.EndCharge();
            _currentWeapon.SetShootPower(shootPower);
            _currentWeapon.Shoot();
            Shooted?.Invoke();
        }
    }

    private void OnWeaponChanged(int weaponIndex)
    {
        if (weaponIndex < 1 || weaponIndex > _weaponTypes.Count)
            Debug.LogError($"Error while changing weapon, can't set {weaponIndex} weapon!");

        WeaponData weaponData = _weaponTypes[weaponIndex - 1];

        ChangeWeapon(weaponData);
    }
}