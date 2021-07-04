using UnityEngine;

namespace AD.Quests
{
    public class QuestCompletion : MonoBehaviour
    {
        [SerializeField] private Quest _quest;
        [SerializeField] private string _objective;
        [SerializeField] QuestList _questList;

        public void CompleteObjective()
        {
            _questList.CompleteObjective(_quest, _objective);
        }
    }
}