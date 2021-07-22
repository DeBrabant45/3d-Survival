using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gather Objective", menuName = "Project/Quests/Objective/GatherObjective", order = 0)]
public class GatherObjective : Objective
{
    [SerializeField] private ItemSO _itemToCollect;
    [SerializeField] private int _amountNeeded;
    [SerializeField] private InventorySystem _inventorySystem;

    public override void Complete()
    {
        isCompleted = _inventorySystem.CheckResourceAvailability(_itemToCollect.ID, _amountNeeded);
        OnComplete?.Invoke(this);
    }

    public override void Evaluate()
    {
        Complete();
    }

    public override void Initialize()
    {
        _inventorySystem = FindObjectOfType<InventorySystem>();
        _inventorySystem.OnInventoryStateChanged += Evaluate;
        Evaluate();
    }

    public override void Terminate()
    {
        _inventorySystem.OnInventoryStateChanged -= Evaluate;
    }
}
