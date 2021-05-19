using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    private Action<IEnemy> _onEnemyDefeated;
    public static QuestEvents Instance;

    public Action<IEnemy> OnEnemyDefeated { get => _onEnemyDefeated; set => _onEnemyDefeated = value; }

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
}
