using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Structure Item Data", menuName = "InventoryData/StructureItemSO")]
public class StructureItemSO : ItemSO
{
    private void OnEnable()
    {
        ItemTypeSO = ItemType.Structure;
    }
}
