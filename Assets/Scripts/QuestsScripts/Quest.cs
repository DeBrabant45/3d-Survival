using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Quest : MonoBehaviour
{
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string Title { get; set; }
    public string Description { get; set; }
    public string QuestGiverName { get; set; }
    public string OnceCompletedInfo { get; set; }
    public int ExperienceReward { get; set; }
    public ItemSO ItemReward { get; set; }
    public bool IsCompleted { get; set; }

    public void CheckGoals()
    {
        IsCompleted = Goals.All(goal => goal.Completed);
    }

    public void GiveReward()
    {
        if(ItemReward != null)
        {
            //Give Reward
        }
        Debug.Log("You've been rewarded!");
    }
}
