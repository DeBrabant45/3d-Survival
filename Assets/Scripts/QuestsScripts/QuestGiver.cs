using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AD.Quests
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] private Quest _quest;

        public void GiveQuest()
        {
            QuestList questLists = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questLists.AddQuest(_quest);
        }
    }
}
