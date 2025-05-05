using UnityEngine;

[RequireComponent(typeof(Animator), typeof(WeaponHolder))]
public class WeaponAnimator : MonoBehaviour
{
    private readonly int ShootParamHash = Animator.StringToHash("Shoot");

    private WeaponHolder _weaponHolder;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _weaponHolder = GetComponent<WeaponHolder>();
    }

    private void OnEnable()
    {
        _weaponHolder.Shooted += OnShoot;
    }

    private void OnDisable()
    {
        _weaponHolder.Shooted -= OnShoot;
    }

    private void OnShoot()
    {
        _animator.SetTrigger(ShootParamHash);
    }
}
