using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Item Data", menuName = "InventoryData/MaterialItemSO")]
public class MaterialItemSO : ItemSO
{
    private void OnEnable()
    {
        ItemTypeSO = ItemType.Material;
    }
}
