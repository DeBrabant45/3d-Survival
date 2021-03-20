using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Item Data", menuName = "InventoryData/MaterialItemSO")]
public class MaterialItemSO : ItemSO
{
    [Header("Item Resource Settings")]
    [SerializeField] private ResourceType resourceType;

    public ResourceType ResourceType { get => resourceType; }

    private void OnEnable()
    {
        ItemTypeSO = ItemType.Material;
    }
}

public enum ResourceType
{
    None,
    Wood,
    Stone
}
