using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged Weapon Item", menuName = "InventoryData/RangedWeaponItemSO")]
public class RangedWeaponItemSO : WeaponItemSO
{
    [Header("Weapon Ammo Settings")]
    [SerializeField] private int _maxAmmoCount;
    [SerializeField] private int _preloadedAmmoAmount;
    [SerializeField] private AmmoItemSO _ammoType;
    [SerializeField] string _reloadAnimationTrigger;
    [SerializeField] string _weaponAimAnimation;

    public int MaxAmmoCount { get => _maxAmmoCount; }
    public int PreloadedAmmoAmount { get => _preloadedAmmoAmount; }
    public AmmoItemSO AmmoType { get => _ammoType; }

    private void OnEnable()
    {
        ItemTypeSO = ItemType.Weapon;
        WeaponTypeSO = WeaponType.Ranged;
    }
}

public enum RangeType
{
    None,
    Shot,
    Thrown,
}
