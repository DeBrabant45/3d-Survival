using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherApples : Quest
{
    private string _questName = "Gather Apples";
    private string _questDescription = "Collect 5 apples";
    private string _appleID = "21780ff88a3d4166bc265904c53e402c";
    private int _currentAmount = 0;
    private int _requiredAmount = 5;
    private GatherGoal _gatherGoal;

    void Start()
    {
        Title = _questName;
        Description = _questDescription;
        OnceCompletedInfo = "Return back to " + QuestGiverName + " to claim your reward";
        _gatherGoal = new GatherGoal(this, _appleID, _questDescription, false, _currentAmount, _requiredAmount);
        Goals.Add(_gatherGoal);
        Goals.ForEach(goal => goal.Init());
    }
}
