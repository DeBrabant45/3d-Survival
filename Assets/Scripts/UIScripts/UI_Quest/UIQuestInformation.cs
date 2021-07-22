using AD.Quests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestInformation : MonoBehaviour
{
    [SerializeField] Text _questTitleText;
    [SerializeField] Text _questGiverNameText;
    [SerializeField] Text _questProgressionText;
    [SerializeField] Text _questRewardText;

    [SerializeField] GameObject _questObjectiveHeader;
    [SerializeField] UIQuestObjective _uIQuestObjective;
    private QuestStatus _questStatus;

    public void Setup(QuestStatus status)
    {
        if (status != null)
        {
            this._questStatus = status;
            _questTitleText.text = _questStatus.GetQuest().name;
            _questObjectiveHeader.SetActive(true);
            _questProgressionText.text = $"Completed: {_questStatus.GetCompletedCount()} / {_questStatus.GetQuest().GetObjectiveCount()}";
            _questRewardText.text = GetRewardsText(_questStatus.GetQuest());
            _uIQuestObjective.CreateQuestObjectives(status);
        }
        else
        {
            _questTitleText.text = "";
            _questGiverNameText.text = "";
            _questObjectiveHeader.SetActive(false);
            _questProgressionText.text = "";
            _questRewardText.text = "";
            _uIQuestObjective.DestroyPanelChildObjects();
        }
    }

    private string GetRewardsText(Quest quest)
    {
        string rewardText = "";
        foreach (var reward in quest.Rewards)
        {
            if(rewardText != "")
            {
                rewardText += ", ";
            }
            if(reward.Number > 1)
            {
                rewardText += reward.Number + " ";
            }
            rewardText += reward.Item.ItemName;
        }
        return rewardText;
    }
}
