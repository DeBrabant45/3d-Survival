using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AD.Quests
{
    public class QuestStatus
    {
        private Quest _quest;
        private List<string> _completedQuestObjectives = new List<string>();
        public Action<QuestStatus> OnCompleteStatus { get; set; }

        public QuestStatus(Quest quest)
        {
            this._quest = quest;
            InitializeQuestObjectives();
        }

        private void InitializeQuestObjectives()
        {
            foreach (var objective in _quest.GetObjectives())
            {
                objective.OnComplete += CheckObjectiveStatus;
                objective.Initialize();
            }
        }

        public QuestStatus(SavedQuestStatusData questStatus)
        {
            _quest = Quest.GetByName(questStatus.QuestName);
            _completedQuestObjectives = questStatus.CompletedObjectives;
        }

        private void CheckObjectiveStatus(Objective objective)
        {
            if (objective.IsCompleted != false && IsObjectiveComplete(objective.ID) == false)
            {
                CompleteObjective(objective.ID);
            }
            else if(objective.IsCompleted == false && IsObjectiveComplete(objective.ID))
            {
                RemoveCompletedObject(objective.ID);
            }
            if(IsComplete())
            {
                OnCompleteStatus?.Invoke(this);
                TerminateQuestObjectives();
            }
        }

        public void TerminateQuestObjectives()
        {
            foreach (var objective in _quest.GetObjectives())
            {
                objective.Terminate();
                objective.OnComplete -= CheckObjectiveStatus;
            }
        }

        private void RemoveCompletedObject(string objective)
        {
            if (_quest.HasObjective(objective))
            {
                _completedQuestObjectives.Remove(objective);
            }
        }

        public Quest GetQuest()
        {
            return _quest;
        }

        public List<string> GetCompletedQuestObjectives()
        {
            return _completedQuestObjectives;
        }

        public int GetCompletedCount()
        {
            return _completedQuestObjectives.Count;
        }

        public bool IsComplete()
        {
            foreach (var objective in _quest.GetObjectives())
            {
                if(_completedQuestObjectives.Contains(objective.ID) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsObjectiveComplete(string objective)
        {
            return _completedQuestObjectives.Contains(objective);
        }

        private void CompleteObjective(string objective)
        {
            if (_quest.HasObjective(objective))
            {
                _completedQuestObjectives.Add(objective);
            }
        }

        public SavedQuestStatusData SaveData()
        {
            var questStatusData = new SavedQuestStatusData
            {
                QuestName = _quest.name,
                CompletedObjectives = _completedQuestObjectives

            };
            return questStatusData;
        }
    }
}
