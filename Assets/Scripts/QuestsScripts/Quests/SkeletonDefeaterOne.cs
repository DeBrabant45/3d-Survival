using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDefeaterOne : Quest
{
    private string _questName = "Skeleton fighter one";
    private string _questDescription = "Defeat one Skeletons";
    private int _enemyID = 0;
    private int _currentDefeatedAmount = 0;
    private int _requiredAmount = 1;
    private DefeatGoal _defeatGoal;

    void Start()
    {
        Title = _questName;
        Description = _questDescription;
        _defeatGoal = new DefeatGoal(this, _enemyID, _questDescription, false, _currentDefeatedAmount, _requiredAmount);
        Goals.Add(_defeatGoal);
        Goals.ForEach(goal => goal.Init());
    }
}
