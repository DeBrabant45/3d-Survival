using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDefeater : Quest
{
    private string _questName = "Skeleton fighter one";
    private string _questDescription = "Defeat three the Skeletons";
    private int enemyID = 0;
    private int _currentDefeatedAmount = 0;
    private int _requiredAmount = 3;
    private DefeatGoal _defeatGoal;

    void Start()
    {
        QuestName = _questName;
        Description = _questDescription;
        _defeatGoal = new DefeatGoal(this, enemyID, _questDescription, false, _currentDefeatedAmount, _requiredAmount);
        Goals.Add(_defeatGoal);
        Goals.ForEach(goal => goal.Init());
    }
}
