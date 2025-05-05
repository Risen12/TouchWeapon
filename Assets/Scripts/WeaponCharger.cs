using System;
using System.Collections;
using UnityEngine;

public class WeaponCharger : MonoBehaviour
{
    [SerializeField] private float _maxPower;
    [SerializeField] private float _minPower;
    [SerializeField] private float _smoothIndex;

    private float _power;
    private Coroutine _chargeCoroutine;

    public float MaxPower => _maxPower;
    public float MinPower => _minPower;

    public event Action ChargeBegan;
    public event Action ChargeEnded;
    public event Action<float> ValueChanged;

    public void BeginCharge()
    {
        _power = 0f;
        ChargeBegan?.Invoke();

        _chargeCoroutine = StartCoroutine(Charge());
    }

    public float EndCharge() 
    {
        if (_chargeCoroutine != null)
            StopCoroutine(_chargeCoroutine);

        ChargeEnded.Invoke();

        return _power;
    }

    private IEnumerator Charge()
    {
        while (true)
        {
            _power = Mathf.MoveTowards(_power, _maxPower, _smoothIndex);
            ValueChanged?.Invoke(_power);

            yield return null;
        }
    }
}
