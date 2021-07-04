using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeater : Quests
{
    [SerializeField] private string _questName;
    [SerializeField] private string _questDescription;
    [SerializeField] private int enemyID;
    [SerializeField] private int _currentDefeatedAmount;
    [SerializeField] private int _requiredAmount;
    [SerializeField] private DefeatGoal _defeatGoal;

    void OnEnable()
    {
        Title = _questName;
        Description = _questDescription;
        _defeatGoal = new DefeatGoal(this, enemyID, _questDescription, false, 0, _requiredAmount);
        Goals.Add(_defeatGoal);
        Goals.ForEach(goal => goal.Init());
    }
}
