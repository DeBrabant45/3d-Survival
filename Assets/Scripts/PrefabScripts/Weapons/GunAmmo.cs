using System;
using UnityEngine;

public class GunAmmo : MonoBehaviour
{
    [SerializeField] RangedWeaponItemSO _rangedWeapon;
    [SerializeField] private int _currentAmmoCount;

    public int CurrentAmmoCount { get => _currentAmmoCount; }

    private void OnEnable()
    {
        _currentAmmoCount = _rangedWeapon.PreloadedAmmoAmount;
    }

    public void AddToCurrentAmmoCount(int amount)
    {
        if (_currentAmmoCount < _rangedWeapon.WeaponMagazineSize)
        {
            _currentAmmoCount += amount;
            RangedWeaponEvents.current.AmmoAmountChange();
        }
    }

    public void RemoveFromCurrentAmmoCount(int amount)
    {
        _currentAmmoCount -= amount;
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
        if (_currentAmmoCount == _rangedWeapon.WeaponMagazineSize)
        {
            return true;
        }
        return false;
    }
}
