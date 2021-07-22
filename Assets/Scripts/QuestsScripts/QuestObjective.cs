using System;

namespace AD.Quests
{
    [Serializable]
    public class QuestObjective
    {
        public string Reference;
        public string Description;
        public Objective Objective;

        public void Init()
        {
            Objective.Initialize();
        }
    }
}
