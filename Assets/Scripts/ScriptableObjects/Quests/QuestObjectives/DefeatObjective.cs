using System;
using UnityEngine;
using System.Collections.Generic;

namespace AD.Quests
{
    [CreateAssetMenu(fileName = "Defeat Objective", menuName = "Project/Quests/Objective/DefeatObjective", order = 0)]
    class DefeatObjective : Objective
    {
        [SerializeField] private int _targetID;
        [SerializeField] private int _amountNeeded;
        [NonSerialized] private int _currentCount;

        public override void Complete()
        {
            isCompleted = true;
            OnComplete?.Invoke(this);
        }

        public override void Evaluate()
        {
            if(_currentCount >= _amountNeeded)
            {
                Complete();
            }
        }

        public override void Initialize()
        {
            QuestEvents.Instance.OnEnemyDefeated += DefeatedOpponent;
        }

        public override void Terminate()
        {
            QuestEvents.Instance.OnEnemyDefeated -= DefeatedOpponent;
        }

        private void DefeatedOpponent(IEnemy enemy)
        {
            if (enemy.EnemyID == this._targetID)
            {
                _currentCount++;
                Evaluate();
            }
        }
    }
}
