using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged Weapon Item", menuName = "InventoryData/RangedWeaponItemSO")]
public class RangedWeaponItemSO : WeaponItemSO
{
    [SerializeField] private int _weaponMagazineSize;
    [SerializeField] private int _preloadedAmmoAmount;

    public int WeaponMagazineSize { get => _weaponMagazineSize; }
    public int PreloadedAmmoAmount { get => _preloadedAmmoAmount; }

    private void OnEnable()
    {
        ItemTypeSO = ItemType.Weapon;
        WeaponTypeSO = WeaponType.Ranged;
    }
}
