using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WeaponChangerButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _butonText;

    private Button _changeWeaponButton;
    private int _weaponsCount;
    private int _currentWeaponIndex;

    public event Action<int> ChangeWeaponButtonClicked;

    private void Awake()
    {
        _changeWeaponButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _changeWeaponButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _changeWeaponButton.onClick.RemoveListener(OnButtonClicked);
    }

    public void SetWeaponsCount(int weaponsCount)
    {
        if (weaponsCount > 0)
        {
            _weaponsCount = weaponsCount;
            _currentWeaponIndex = 1;
        }
        else
        {
            Debug.LogError("Weapons count couldn't be zero!");
        }

        SetButtonText();
    }

    private void OnButtonClicked()
    {
        if (_weaponsCount == 1)
            return;

        ChangeWeaponIndex();
        SetButtonText();

        ChangeWeaponButtonClicked?.Invoke(_currentWeaponIndex);
    }

    private int DefineNextWeaponIndex()
    {
        int nextWeaponIndex = 1;

        if (_currentWeaponIndex + 1 > _weaponsCount)
        {
            nextWeaponIndex = 1;
        }
        else
        {
            nextWeaponIndex = _currentWeaponIndex + 1;
        }

        return nextWeaponIndex;
    }

    private void SetButtonText()
    {
        int nextWeaponIndex = DefineNextWeaponIndex();

        _butonText.text = $"Переключить на тип {nextWeaponIndex}";
    }

    private void ChangeWeaponIndex()
    {
        if (_currentWeaponIndex + 1 > _weaponsCount)
        {
            _currentWeaponIndex = 1;
        }
        else
        {
            _currentWeaponIndex++;
        }
    }
}
