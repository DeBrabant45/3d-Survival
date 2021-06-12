using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    private Action<IEnemy> _onEnemyDefeated;
    private Action<Quest> _onCompletedQuest;
    private Action<Quest> _onAddedQuest;
    public static QuestEvents Instance;

    public Action<IEnemy> OnEnemyDefeated { get => _onEnemyDefeated; set => _onEnemyDefeated = value; }
    public Action<Quest> OnCompletedQuest { get => _onCompletedQuest; set => _onCompletedQuest = value; }
    public Action<Quest> OnAddedQuest { get => _onAddedQuest; set => _onAddedQuest = value; }

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

    public void CompletedQuest(Quest quest)
    {
        _onCompletedQuest?.Invoke(quest);
    }

    public void AddedQuest(Quest quest)
    {
        _onAddedQuest?.Invoke(quest);
    }
}
