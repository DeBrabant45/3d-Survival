using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeater : Quest
{
    [SerializeField] private string _questName;
    [SerializeField] private string _questDescription;
    [SerializeField] private IEnemy enemy;
    [SerializeField] private int _enemyID;
    [SerializeField] private int _currentDefeatedAmount;
    [SerializeField] private int _requiredAmount;
    private DefeatGoal _defeatGoal;

    void Start()
    {
        QuestName = _questName;
        Description = _questDescription;
        _defeatGoal = new DefeatGoal(this, enemy.EnemyID, _questDescription, false, _currentDefeatedAmount, _requiredAmount);
        Goals.Add(_defeatGoal);
        Goals.ForEach(goal => goal.Init());
    }
}
