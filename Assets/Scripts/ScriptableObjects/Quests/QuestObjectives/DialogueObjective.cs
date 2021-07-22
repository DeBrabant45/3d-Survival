using AD.Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Objective", menuName = "Project/Quests/Objective/DialogueObjective", order = 0)]
public class DialogueObjective : Objective
{
    public override void Complete()
    {
        isCompleted = true;
        OnComplete?.Invoke(this);
    }

    public override void Evaluate()
    {

    }

    private void CheckCompleted(DialogueObjective objective)
    {
        if (objective == this)
        {
            Complete();
        }
    }

    public override void Initialize()
    {
        QuestEvents.Instance.OnCompleteDialogueObjective += CheckCompleted;
    }

    public override void Terminate()
    {
        QuestEvents.Instance.OnCompleteDialogueObjective -= CheckCompleted;
    }
}
