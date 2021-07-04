using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UIScripts.UI_Quest
{
    public class UIQuestAdded : MonoBehaviour
    {
        [SerializeField] GameObject _addedQuestPrefab;
        [SerializeField] Transform _addedQuestPanel;

        private void Start()
        {
            QuestEvents.Instance.OnAddedQuest += DisplayAddedUI;
        }

        private void DisplayAddedUI(Quests quest)
        {
            Debug.Log(quest.Title);
            var panelGameObject = Instantiate(_addedQuestPrefab, _addedQuestPanel);
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