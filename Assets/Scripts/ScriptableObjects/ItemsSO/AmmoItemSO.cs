using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generic Ammo Item", menuName = "InventoryData/AmmoItemSO")]
public class AmmoItemSO : ItemSO
{
    public override bool IsUsable()
    {
        return true;
    }

    private void OnEnable()
    {
        ItemTypeSO = ItemType.Ammo;
    }
}
