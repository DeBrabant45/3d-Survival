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
    private Quests _quest;

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
        _quest = (Quests)_activeQuests.AddComponent(Type.GetType(_questType.ToString()));
        _quest.QuestGiverName = _characterName;
        QuestEvents.Instance.AddedQuest(_quest);
    }

    private void CompletedQuest()
    {
        _quest.GiveReward();
        QuestEvents.Instance.CompletedQuest(_quest);
        IsGivenQuestCompleted = true;
        IsQuestAssigned = false;
        Destroy((Quests)_activeQuests.GetComponent(Type.GetType(_questType.ToString())));
        _quest = (Quests)_completedQuests.AddComponent(Type.GetType(_questType.ToString()));
        _quest.QuestGiverName = _characterName;
    }

    private void CheckCurrentQuest()
    {
        if (_quest.IsCompleted)
        {
            _dialogueSystem.AddNewDialogue(_questCompletedDialogue, _characterName);
            CompletedQuest();
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
    GatherApples
}
