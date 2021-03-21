using System;
using UnityEngine;

public class Ammo : MonoBehaviour, IAmmo
{
    [SerializeField] RangedWeaponItemSO _rangedWeapon;
    [SerializeField] private int _currentAmmoCount;

    public int CurrentAmmoCount { get => _currentAmmoCount; }

    private void Awake()
    {
        _currentAmmoCount = _rangedWeapon.PreloadedAmmoAmount;
        RangedWeaponEvents.current.RangedWeaponAmmoAmountChange(_currentAmmoCount);
    }

    public void ReloadAmmoCount()
    {
        if (_currentAmmoCount < _rangedWeapon.MaxAmmoCount)
        {
            _currentAmmoCount = _rangedWeapon.MaxAmmoCount;
            RangedWeaponEvents.current.RangedWeaponAmmoAmountChange(_currentAmmoCount);
        }
    }

    public void RemoveFromCurrentAmmoCount()
    {
        _currentAmmoCount--;
        RangedWeaponEvents.current.RangedWeaponAmmoAmountChange(_currentAmmoCount);
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
