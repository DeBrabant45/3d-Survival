using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIQuestLog : MonoBehaviour
{
    [SerializeField] private GameObject _activeQuests;
    [SerializeField] private GameObject _completedQuests;
    [SerializeField] private GameObject _questLogPanel;

    [SerializeField] private Transform _questPickerPanel;

    [SerializeField] private Button _activeQuestBtn;
    [SerializeField] private Button _completedQuestsBtn;
    [SerializeField] private Button _questPickerBtnPrefab;

    [SerializeField] private Text _questTitle;
    [SerializeField] private Text _questGiverName;
    [SerializeField] private Text _questDescription;

    void Start()
    {
        _questLogPanel.SetActive(false);
        _activeQuestBtn.onClick.AddListener(SetActiveQuestInfo);
        _completedQuestsBtn.onClick.AddListener(SetCompletedQuestInfo);
    }

    private void SetActiveQuestInfo()
    {
        DestroyPanelChildObjects();
        var quests = _activeQuests.GetComponents<Quest>();
        if (quests != null)
        {
            CreateButtons(quests);
        }
    }

    private void SetCompletedQuestInfo()
    {
        DestroyPanelChildObjects();
        var quests = _completedQuests.GetComponents<Quest>();
        if(quests != null)
        {
            CreateButtons(quests);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuestLogPanel();
        }
    }

    public void ActivateQuestPanel()
    {
        _questLogPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SetActiveQuestInfo();
    }

    public void DeativateQuestPanel()
    {
        DestroyPanelChildObjects();
        _questLogPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ToggleQuestLogPanel()
    {
        if(_questLogPanel.activeSelf == false)
        {
            ActivateQuestPanel();
        }
        else
        {
            DeativateQuestPanel();
        }
    }

    public void CreateButtons(Quest[] quests)
    {
        foreach (var quest in quests)
        {
            var btn = _questPickerBtnPrefab;
            btn.GetComponentInChildren<Text>().text = quest.QuestName;
            var newBtn = Instantiate(btn, _questPickerPanel);
            newBtn.onClick.AddListener(() => SetQuest(quest));
        }
    }

    private void SetQuest(Quest quest)
    {
        _questTitle.text = quest.QuestName;
        _questDescription.text = quest.Description;
    }

    private void DestroyPanelChildObjects()
    {
        foreach (Transform questBtn in _questPickerPanel)
        {
            Destroy(questBtn.gameObject);
        }
    }
}
