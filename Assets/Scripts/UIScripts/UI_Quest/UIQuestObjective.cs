using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AD.Quests;

public class UIQuestObjective : MonoBehaviour
{
    [SerializeField] GameObject _questObjectiveIncompeletePrefab;
    [SerializeField] GameObject _questObjectiveCompeletedPrefab;

    public void CreateQuestObjectives(QuestStatus status)
    {
        DestroyPanelChildObjects();
        if(status != null)
        {
            foreach (var objective in status.GetQuest().GetObjectives())
            {
                GameObject prefab = null;
                if (status.IsObjectiveComplete(objective.ID))
                {
                    prefab = _questObjectiveCompeletedPrefab;
                }
                else
                {
                    prefab = _questObjectiveIncompeletePrefab;
                }
                GameObject objectivePrefab = Instantiate(prefab, this.transform);
                Text objectiveText = objectivePrefab.GetComponentInChildren<Text>();
                if (objectiveText != null)
                {
                    objectiveText.text = objective.Description;
                }
            }
        }
    }

    public void DestroyPanelChildObjects()
    {
        foreach (Transform gameobject in this.transform)
        {
            Destroy(gameobject.gameObject);
        }
    }
}
