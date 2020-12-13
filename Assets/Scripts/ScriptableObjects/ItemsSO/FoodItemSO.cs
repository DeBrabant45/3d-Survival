using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generic Food Item", menuName = "InventoryData/FoodItemSO")]
public class FoodItemSO : ItemSO
{
    [SerializeField] int _staminaBonus = 0;
    [SerializeField] int _hungerBonus = 0;
    [SerializeField] int _energyBonus = 0;

    public override bool IsUsable()
    {
        return true;
    }
}
