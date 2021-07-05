using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AD.Quests
{
    public class QuestList : MonoBehaviour, ISaveable
    {
        private List<QuestStatus> _statuses = new List<QuestStatus>();

        public Action OnUpdate { get; set; }

        public void AddQuest(Quest quest)
        {
            if(HasQuest(quest))
            {
                return;
            }
            var newStatus = new QuestStatus(quest);
            _statuses.Add(newStatus);
            OnUpdate?.Invoke();
        }

        public void CompleteObjective(Quest quest, string objective)
        {
            QuestStatus status = GetQuestStatus(quest);
            status.CompleteObjective(objective);
            OnUpdate?.Invoke();
        }

        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        private QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (var status in _statuses)
            {
                if (status.GetQuest() == quest)
                {
                    return status;
                }
            }
            return null;
        }

        public QuestStatus GetStatusesRoot()
        {
            return _statuses[0];
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return _statuses;
        }

        public string GetJsonDataToSave()
        {
            List<SavedQuestStatusData> savedQuestStatuses = new List<SavedQuestStatusData>();
            foreach (QuestStatus status in _statuses)
            {
                savedQuestStatuses.Add(status.SaveData());
            }
            string data = JsonConvert.SerializeObject(savedQuestStatuses);
            return data;
        }

        public void LoadJsonData(string jsonData)
        {
            List<SavedQuestStatusData> savedQuestStatuses = JsonConvert.DeserializeObject<List<SavedQuestStatusData>>(jsonData);
            if (savedQuestStatuses == null)
            {
                return;
            }
            _statuses.Clear();
            foreach (SavedQuestStatusData savedQuestStatus in savedQuestStatuses)
            {
                _statuses.Add(new QuestStatus(savedQuestStatus));
            }
        }
    }
}
