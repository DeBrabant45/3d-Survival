using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged Weapon Item", menuName = "InventoryData/RangedWeaponItemSO")]
public class RangedWeaponItemSO : WeaponItemSO
{
    [SerializeField] private int _maxAmmoCount;
    [SerializeField] private int _startCurrentAmmoCount;

    public int MaxAmmoCount { get => _maxAmmoCount; }
    public int StartCurrentAmmoCount { get => _startCurrentAmmoCount; }

    private void OnEnable()
    {
        ItemTypeSO = ItemType.Weapon;
        WeaponTypeSO = WeaponType.Ranged;
    }
}
