using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangedWeaponEvents : MonoBehaviour
{
    public static RangedWeaponEvents Instance;
    public event Action<RangedWeaponItemSO> OnRangedWeaponEquipped;
    public event Action OnRangedWeaponUnequipped;
    public event Action OnInventoryHasChanged;
    public event Action<int> OnRangedWeaponAmmoAmmountChange;
    public event Action<Vector3> OnRangedWeaponIsFiring;

    private void Awake()
    {
        Instance = this;
    }
    
    public void RangedWeaponIsFiring(Vector3 position)
    {
        OnRangedWeaponIsFiring?.Invoke(position);
    }

    public void RangedWeaponAmmoAmountChange(int amount)
    {
        OnRangedWeaponAmmoAmmountChange?.Invoke(amount);
    }    

    public void RangedWeaponEquipped(RangedWeaponItemSO rangedWeapon)
    {
        OnRangedWeaponEquipped?.Invoke(rangedWeapon);
    }

    public void RangedWeaponUnequipped()
    {
        OnRangedWeaponUnequipped?.Invoke();
    }

    public void InventoryHasChanged()
    {
        OnInventoryHasChanged?.Invoke();
    }
}
