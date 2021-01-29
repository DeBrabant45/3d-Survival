using System;
using UnityEngine;

public class GunAmmo : MonoBehaviour
{
    [SerializeField] RangedWeaponItemSO _rangedWeapon;
    [SerializeField] private int _currentAmmoCount;

    public int CurrentAmmoCount { get => _currentAmmoCount; }

    private void Awake()
    {
        _currentAmmoCount = _rangedWeapon.PreloadedAmmoAmount;
    }

    public void ReloadAmmoCount()
    {
        if (_currentAmmoCount < _rangedWeapon.MaxAmmoCount)
        {
            _currentAmmoCount = _rangedWeapon.MaxAmmoCount;
            RangedWeaponEvents.current.AmmoAmountChange();
        }
    }

    public void RemoveFromCurrentAmmoCount()
    {
        _currentAmmoCount--;
        RangedWeaponEvents.current.AmmoAmountChange();
    }

    public bool IsAmmoEmpty()
    {
        if (_currentAmmoCount <= 0)
        {
            return true;
        }
        return false;
    }

    public bool IsAmmoFull()
    {
        if (_currentAmmoCount == _rangedWeapon.MaxAmmoCount)
        {
            return true;
        }
        return false;
    }
}
