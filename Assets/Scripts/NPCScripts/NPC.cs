using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IUsable
{
    [SerializeField] private string[] _dialogue;
    [SerializeField] private string _characterName;
    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private GameObject _quests;
    [SerializeField] private string _questType;

    [SerializeField] private GameObject _activeQuests;
    [SerializeField] private GameObject _completedQuests;
    private Quest _quest;

    public bool IsQuestAssigned { get; set; }
    public bool IsGivenQuestCompleted { get; set; }

    public void Use()
    {
        if(IsQuestAssigned == false && IsGivenQuestCompleted == false)
        {
            _dialogueSystem.AddNewDialogue(_dialogue, _characterName);
            AssignQuest();
        }
        else if(IsQuestAssigned && IsGivenQuestCompleted == false)
        {
            CheckCurrentQuest();
        }
        else
        {
            _dialogueSystem.AddNewDialogue(new string[] { "Dude I have more work anytime you want" }, _characterName);
        }
    }

    private void AssignQuest()
    {
        IsQuestAssigned = true;
        _quest = (Quest)_activeQuests.AddComponent(Type.GetType(_questType));
    }

    private void CompletedQuest()
    {
        _quest.GiveReward();
        IsGivenQuestCompleted = true;
        IsQuestAssigned = false;
        Destroy((Quest)_activeQuests.GetComponent(Type.GetType(_questType)));
        _quest = (Quest)_completedQuests.AddComponent(Type.GetType(_questType));
    }

    private void CheckCurrentQuest()
    {
        if(_quest.IsCompleted)
        {
            CompletedQuest();
            _dialogueSystem.AddNewDialogue(new string[] { "Thank you" }, _characterName);
        }
        else
        {
            _dialogueSystem.AddNewDialogue(new string[] { "Get Back at it" }, _characterName);
        }
    }
}
