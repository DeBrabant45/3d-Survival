using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool Item Data", menuName = "InventoryData/ToolItemSO")]
public class ToolItemSO : WeaponItemSO
{
    [SerializeField] private int _baseHarvestPower = 3;
    [SerializeField] private ResourceType _resourceBoosted;
    [SerializeField] private int _boostedHarevestPower = 6;

    public int GetResourceHarvested(ResourceType resourceType)
    {
        if(_resourceBoosted == resourceType)
        {
            return _boostedHarevestPower;
        }
        return _baseHarvestPower;
    }

    private void OnEnable()
    {
        ItemTypeSO = ItemType.Weapon;
    }
}

public enum ResourceType
{
    None,
    Wood,
    Stone
}
