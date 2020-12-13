using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generic Weapon Item", menuName = "InventoryData/WeaponItemSO")]
public class WeaponItemSO : ItemSO
{
    [SerializeField] int _minimalDamage = 0;
    [SerializeField] int _maximumDamage = 0;
    [SerializeField] [Range(0, 1)] float _criticalDamageChance = 0.2f;
}
