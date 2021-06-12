using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UIScripts.UI_Quest
{
    public class UIQuestCompleted : MonoBehaviour
    {
        [SerializeField] GameObject _completedQuestPrefab;
        [SerializeField] Transform _completedQuestPanel;

        private void Start()
        {
            QuestEvents.Instance.OnCompletedQuest += DisplayCompletedUI;
        }

        private void DisplayCompletedUI(Quest quest)
        {
            var panelGameObject = Instantiate(_completedQuestPrefab, _completedQuestPanel);
            foreach (Transform child in panelGameObject.transform)
            {
                var childText = child.GetComponentInChildren<Text>();
                if (childText != null)
                {
                    childText.text = quest.Title + "";
                }
            }
            Destroy(panelGameObject, 4f);
        }
    }
}