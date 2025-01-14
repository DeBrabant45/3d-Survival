using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDefeaterOne : Quests
{
    private string _questName = "Skeleton fighter one";
    private string _questDescription = "Defeat one Skeleton";
    private int _enemyID = 0;
    private int _currentDefeatedAmount = 0;
    private int _requiredAmount = 1;
    private DefeatGoal _defeatGoal;

    private void Awake()
    {
        Title = _questName;
        Description = _questDescription;
        OnceCompletedInfo = "Return back to " + QuestGiverName + " to claim your reward";
        _defeatGoal = new DefeatGoal(this, _enemyID, _questDescription, false, _currentDefeatedAmount, _requiredAmount);
        Goals.Add(_defeatGoal);
        Goals.ForEach(goal => goal.Init());
    }
}
