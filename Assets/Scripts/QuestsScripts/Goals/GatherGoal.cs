using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherGoal : Goal
{
    private string _targetID;

    public GatherGoal(Quest quest, string targetID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this._targetID = targetID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        InventoryEvents.Instance.OnItemCollected += GatherItem;
    }

    private void GatherItem(ItemSO item, int amount)
    {
        if (item.ID == this._targetID)
        {
            CurrentAmount += amount;
            EvaluateAmount();
        }
    }
}
