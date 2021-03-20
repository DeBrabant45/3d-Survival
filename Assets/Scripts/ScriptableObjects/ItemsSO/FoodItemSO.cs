using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generic Food Item", menuName = "InventoryData/FoodItemSO")]
public class FoodItemSO : ItemSO
{
    [Header("Food Restore Settings")]
    [SerializeField] int _staminaBonus = 0;
    [SerializeField] int _healthBonus = 0;
    [SerializeField] int _energyBonus = 0;

    public int StaminaBonus { get => _staminaBonus; }
    public int HealthBonus { get => _healthBonus; }

    public override bool IsUsable()
    {
        return true;
    }
}
