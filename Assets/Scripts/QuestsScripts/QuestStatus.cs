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

        public Quest GetQuest()
        {
            return _quest;
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
    }
}
