using System;
using UnityEngine;

[RequireComponent(typeof(WeaponChangerButton))]
public class WeaponChanger : MonoBehaviour
{
    private WeaponChangerButton _weaponChangerButton;

    public event Action<int> WeaponChanged;

    private void Awake()
    {
        _weaponChangerButton = GetComponent<WeaponChangerButton>();
    }

    private void OnEnable()
    {
        _weaponChangerButton.ChangeWeaponButtonClicked += OnChangeWeaponButtonClicked;
    }

    private void OnDisable()
    {
        _weaponChangerButton.ChangeWeaponButtonClicked -= OnChangeWeaponButtonClicked;
    }

    public void Initialize(int weaponsCount) => _weaponChangerButton.SetWeaponsCount(weaponsCount);

    private void OnChangeWeaponButtonClicked(int currentWeaponIndex) => WeaponChanged?.Invoke(currentWeaponIndex);
}
