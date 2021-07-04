using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AD.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Project/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] List<string> _objectives = new List<string>();

        public string GetTitle()
        {
            return name;
        }

        public IEnumerable<string> GetObjectives()
        {
            return _objectives;
        }

        public int GetObjectiveCount()
        {
            return _objectives.Count;
        }

        public bool HasObjective(string objective)
        {
            return _objectives.Contains(objective);
        }
    }
}
