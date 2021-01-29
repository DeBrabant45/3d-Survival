using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangedWeaponEvents : MonoBehaviour
{
    public static RangedWeaponEvents current;
    public event Action onAmmoAmountChange;
    public event Action onRangedWeaponEquipped;
    public event Action onRangedWeaponUnequipped;

    private void Awake()
    {
        current = this;
    } 

    public void AmmoAmountChange()
    {
        onAmmoAmountChange?.Invoke();
    }

    public void RangedWeaponEquipped()
    {
        onRangedWeaponEquipped?.Invoke();
    }

    public void RangedWeaponUnequipped()
    {
        onRangedWeaponUnequipped?.Invoke();
    }
}
