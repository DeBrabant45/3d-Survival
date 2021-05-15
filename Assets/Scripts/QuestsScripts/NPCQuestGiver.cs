using System;
using UnityEngine;

public class NPCQuestGiver : NPC
{
    [Header("Quest Dialogue")]
    [SerializeField] private string[] _questDialogue;
    [SerializeField] private string[] _questCompletedDialogue;
    [SerializeField] private string[] _questIncompleteDialogue;
    [SerializeField] private string[] _questCompletedReturnDialogue;

    [Header("Quest Info")]
    [SerializeField] private QuestType _questType;

    [Header("Quest Scene GameObjects")]
    [SerializeField] private GameObject _activeQuests;
    [SerializeField] private GameObject _completedQuests;
    private Quest _quest;

    public bool IsQuestAssigned { get; set; }
    public bool IsGivenQuestCompleted { get; set; }

    public override void Use()
    {
        if (IsQuestAssigned == false && IsGivenQuestCompleted == false)
        {
            _dialogueSystem.AddNewDialogue(_questDialogue, _characterName);
            AssignQuest();
        }
        else if (IsQuestAssigned && IsGivenQuestCompleted == false)
        {
            Debug.Log(_quest);
            CheckCurrentQuest();
        }
        else
        {
            _dialogueSystem.AddNewDialogue(_questCompletedReturnDialogue, _characterName);
        }
    }

    private void AssignQuest()
    {
        IsQuestAssigned = true;
        _quest = (Quest)_activeQuests.AddComponent(Type.GetType(_questType.ToString()));
        _quest.QuestGiverName = _characterName;
    }

    private void CompletedQuest()
    {
        _quest.GiveReward();
        IsGivenQuestCompleted = true;
        IsQuestAssigned = false;
        Destroy((Quest)_activeQuests.GetComponent(Type.GetType(_questType.ToString())));
        _quest = (Quest)_completedQuests.AddComponent(Type.GetType(_questType.ToString()));
        _quest.QuestGiverName = _characterName;
    }

    private void CheckCurrentQuest()
    {
        if (_quest.IsCompleted)
        {
            CompletedQuest();
            _dialogueSystem.AddNewDialogue(_questCompletedDialogue, _characterName);
        }
        else
        {
            _dialogueSystem.AddNewDialogue(_questIncompleteDialogue, _characterName);
        }
    }
}

public enum QuestType
{
    None,
    SkeletonDefeaterOne,
    SkeletonDefeaterTwo,
}
