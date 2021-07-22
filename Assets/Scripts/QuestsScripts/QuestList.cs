using AD.General;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AD.Quests
{
    public class QuestList : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        private List<QuestStatus> _activeStatuses = new List<QuestStatus>();
        private List<QuestStatus> _completedStatuse = new List<QuestStatus>();

        public Action OnUpdate { get; set; }

        public void AddQuest(Quest quest)
        {
            if(HasActiveQuest(quest.GetTitle()) || HasCompletedQuest(quest.GetTitle()))
            {
                return;
            }
            var newStatus = new QuestStatus(quest);
            newStatus.OnCompleteStatus += AddCompletedStatus;
            _activeStatuses.Add(newStatus);
            OnUpdate?.Invoke();
        }

        private void AddCompletedStatus(QuestStatus status)
        {
            GiveReward(status.GetQuest());
            _completedStatuse.Add(status);
            _activeStatuses.Remove(status);
            status.OnCompleteStatus -= AddCompletedStatus;
            OnUpdate?.Invoke();
        }

        private void GiveReward(Quest quest)
        {
            foreach (var reward in quest.Rewards)
            {
                ItemSpawnManager.Instance.CreateItemInPlace(this.transform.position, reward.Item, reward.Number);
            }
        }

        public bool HasActiveQuest(string questTitle)
        {
            return GetActiveQuest(questTitle) != null;
        }

        public bool HasCompletedQuest(string quest)
        {
            return GetCompletedQuest(quest) != null;
        }

        private QuestStatus GetActiveQuest(string questTitle)
        {
            return _activeStatuses.FirstOrDefault(status => status.GetQuest().GetTitle() == questTitle);
        }

        private QuestStatus GetCompletedQuest(string questTitle)
        {
            return _completedStatuse.FirstOrDefault(status => status.GetQuest().GetTitle() == questTitle);
        }

        private bool HasCompletedObjective(string objectiveID)
        {
            return GetCompletedObjective(objectiveID) != null;
        }

        private bool HasActiveObjective(string objectiveID)
        {
            return GetActiveObjective(objectiveID) != null;
        }

        private QuestStatus GetActiveObjective(string objectiveID)
        {
            return _activeStatuses.FirstOrDefault(status => status.GetCompletedQuestObjectives().Contains(objectiveID));
        }

        private QuestStatus GetCompletedObjective(string objectiveID)
        {
            return _completedStatuse.FirstOrDefault(status => status.GetCompletedQuestObjectives().Contains(objectiveID));
        }

        public QuestStatus GetStatusesRoot()
        {
            return _activeStatuses[0];
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return _activeStatuses;
        }

        public QuestStatus GetCompletedStatusesRoot()
        {
            return _completedStatuse[0];
        }

        public IEnumerable<QuestStatus> GetCompletedStatuses()
        {
            return _completedStatuse;
        }

        public string GetJsonDataToSave()
        {
            List<SavedQuestStatusData> savedQuestStatuses = new List<SavedQuestStatusData>();
            foreach (QuestStatus status in _activeStatuses)
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
            _activeStatuses.Clear();
            foreach (SavedQuestStatusData savedQuestStatus in savedQuestStatuses)
            {
                _activeStatuses.Add(new QuestStatus(savedQuestStatus));
            }
        }

        public bool? Evaluate(Predicate predicate, string[] parameters)
        {
            switch (predicate)
            {
                case Predicate.HasEverAcceptedQuest:
                    return HasActiveQuest(parameters[0]) || HasCompletedQuest(parameters[0]);
                case Predicate.HasActiveQuest:
                    return HasActiveQuest(parameters[0]);
                case Predicate.HasCompletedQuest:
                    return HasCompletedQuest(parameters[0]);
                case Predicate.HasCompletedObjective:
                    return HasActiveObjective(parameters[0]) || HasCompletedObjective(parameters[0]);
            }
            return null;
        }
    }
}
