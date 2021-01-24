using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAmmo : MonoBehaviour
{
    [SerializeField] RangedWeaponItemSO _rangedWeapon;
    [SerializeField] private int _currentAmmoCount;

    public int CurrentAmmoCount { get => _currentAmmoCount; }

    private void Start()
    {
        _currentAmmoCount = _rangedWeapon.StartCurrentAmmoCount;
    }

    public void AddToCurrentAmmoCount(int amount)
    {
        if (_currentAmmoCount < _rangedWeapon.MaxAmmoCount)
        {
            _currentAmmoCount += amount;
        }
    }

    public void RemoveFromCurrentAmmoCount(int amount)
    {
        _currentAmmoCount -= amount;
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
