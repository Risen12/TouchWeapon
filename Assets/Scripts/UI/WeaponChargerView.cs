using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponCharger))]
public class WeaponChargerView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private WeaponCharger _weaponCharger;

    private void Awake()
    {
        _weaponCharger = GetComponent<WeaponCharger>();

        _slider.maxValue = _weaponCharger.MaxPower;
        _slider.minValue = _weaponCharger.MinPower;
        _slider.value = _slider.minValue;
        _slider.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _weaponCharger.ChargeBegan += OnChargeBegan;
        _weaponCharger.ChargeEnded += OnChargeEnded;
        _weaponCharger.ValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _weaponCharger.ChargeBegan -= OnChargeBegan;
        _weaponCharger.ChargeEnded -= OnChargeEnded;
        _weaponCharger.ValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(float value)
    { 
        _slider.value = value;
    }

    private void OnChargeBegan()
    {
        _slider.value = _slider.minValue;
        _slider.gameObject.SetActive(true);
    }

    private void OnChargeEnded()
    {
        _slider.gameObject.SetActive(false);
    }
}
