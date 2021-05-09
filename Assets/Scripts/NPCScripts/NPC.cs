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
        _quest = (Quest)_quests.AddComponent(Type.GetType(_questType));
    }

    private void CheckCurrentQuest()
    {
        if(_quest.IsCompleted)
        {
            _quest.GiveReward();
            IsGivenQuestCompleted = true;
            IsQuestAssigned = false;
            _dialogueSystem.AddNewDialogue(new string[] { "Thank you" }, _characterName);
        }
        else
        {
            _dialogueSystem.AddNewDialogue(new string[] { "Get Back at it" }, _characterName);
        }
    }
}
