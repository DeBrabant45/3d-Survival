using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatGoal : Goal
{
    private int _targetID;

    public DefeatGoal(Quests quest, int targetID, string description, bool completed, int currentAmount, int requiredAmount)
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
        QuestEvents.Instance.OnEnemyDefeated += DefeatedOpponent;
    }

    private void DefeatedOpponent(IEnemy enemy)
    {
        if(enemy.EnemyID == this._targetID)
        {
            this.CurrentAmount++;
            EvaluateAmount();
        }
    }
}
