using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDefeaterTwo : Quest
{
    private string _questName = "Skeleton fighter two";
    private string _questDescription = "Defeat two Skeletons";
    private int _enemyID = 0;
    private int _currentDefeatedAmount = 0;
    private int _requiredAmount = 2;
    private DefeatGoal _defeatGoal;

    void Start()
    {
        Title = _questName;
        Description = _questDescription;
        OnceCompletedInfo = "Return back to " + QuestGiverName + " to claim your reward";
        _defeatGoal = new DefeatGoal(this, _enemyID, _questDescription, false, _currentDefeatedAmount, _requiredAmount);
        Goals.Add(_defeatGoal);
        Goals.ForEach(goal => goal.Init());
    }
}
