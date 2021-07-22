using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AD.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Project/Quests/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] private List<Objective> _objectives = new List<Objective>();
        [SerializeField] private List<Reward> _rewards = new List<Reward>();

        public List<Reward> Rewards { get => _rewards; }

        public string GetTitle()
        {
            return name;
        }

        public IEnumerable<Objective> GetObjectives()
        {
            return _objectives;
        }

        public int GetObjectiveCount()
        {
            return _objectives.Count;
        }

        public bool HasObjective(string objectiveID)
        {
            foreach (var objective in _objectives)
            {
                if (objective.ID == objectiveID)
                {
                    return true;
                }
            }
            return false;
        }

        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == questName)
                {
                    return quest;
                }
            }
            return null;
        }
    }
}
