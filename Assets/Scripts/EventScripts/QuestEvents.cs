using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    private Action<IEnemy> _onEnemyDefeated;
    private Action<Quests> _onCompletedQuest;
    private Action<Quests> _onAddedQuest;
    private Action<DialogueObjective> _onCompleteDialogueObjective;
    public static QuestEvents Instance;

    public Action<IEnemy> OnEnemyDefeated { get => _onEnemyDefeated; set => _onEnemyDefeated = value; }
    public Action<Quests> OnCompletedQuest { get => _onCompletedQuest; set => _onCompletedQuest = value; }
    public Action<Quests> OnAddedQuest { get => _onAddedQuest; set => _onAddedQuest = value; }
    public Action<DialogueObjective> OnCompleteDialogueObjective { get => _onCompleteDialogueObjective; set => _onCompleteDialogueObjective = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void EnemyDefeated(IEnemy enemy)
    {
        _onEnemyDefeated?.Invoke(enemy);
    }

    public void CompletedQuest(Quests quest)
    {
        _onCompletedQuest?.Invoke(quest);
    }

    public void AddedQuest(Quests quest)
    {
        _onAddedQuest?.Invoke(quest);
    }

    public void CompleteDialogueObjective(DialogueObjective objective)
    {
        _onCompleteDialogueObjective?.Invoke(objective);
    }
}
