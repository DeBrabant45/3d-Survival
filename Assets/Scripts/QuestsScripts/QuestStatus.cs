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

        public QuestStatus(Quest quest)
        {
            this._quest = quest;
        }

        public QuestStatus(SavedQuestStatusData questStatus)
        {
            _quest = Quest.GetByName(questStatus.QuestName);
            _completedQuestObjectives = questStatus.CompletedObjectives;
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

        public bool IsObjectiveComplete(string objective)
        {
            return _completedQuestObjectives.Contains(objective);
        }

        public void CompleteObjective(string objective)
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
