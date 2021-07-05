using System;
using System.Collections.Generic;

namespace AD.Quests
{
    [Serializable]
    public struct SavedQuestStatusData
    {
        public string QuestName;
        public List<string> CompletedObjectives;
    }
}
