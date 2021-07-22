using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using AD.Quests;

public class UIQuestLog : MonoBehaviour
{
    [SerializeField] private GameObject _questLogPanel;
    [SerializeField] private GameObject _questObjectPrefab;

    [SerializeField] private Transform _questPickerPanel;

    [SerializeField] private Button _activeQuestBtn;
    [SerializeField] private Button _completedQuestsBtn;
    [SerializeField] private Button _questPickerBtnPrefab;

    [SerializeField] private UIQuestInformation _uIQuestInformationPanel;
    [SerializeField] private QuestList _questList;

    void Start()
    {
        _questLogPanel.SetActive(false);
        _activeQuestBtn.onClick.AddListener(() => SetQuestInfoPanel());
        _completedQuestsBtn.onClick.AddListener(() => SetCompletedQuestInfoPanel());
    }
    
    private void SetQuestInfoPanel()
    {
        DestroyPanelChildObjects(_questPickerPanel);
        if (_questList.GetStatuses().Count() > 0)
        {
            SetQuest(_questList.GetStatusesRoot());
            CreateButtons(_questList.GetStatuses());
        }
        else
        {
            SetQuest(null);
        }
    }

    private void SetCompletedQuestInfoPanel()
    {
        DestroyPanelChildObjects(_questPickerPanel);
        if (_questList.GetCompletedStatuses().Count() > 0)
        {
            SetQuest(_questList.GetCompletedStatusesRoot());
            CreateButtons(_questList.GetCompletedStatuses());
        }
        else
        {
            SetQuest(null);
        }
    }

    private void CreateButtons(IEnumerable<QuestStatus> quests)
    {
        foreach (var quest in quests)
        {
            var btn = _questPickerBtnPrefab;
            btn.GetComponentInChildren<Text>().text = quest.GetQuest().name;
            var newBtn = Instantiate(btn, _questPickerPanel);
            newBtn.onClick.AddListener(() => SetQuest(quest));
        }
    }

    private void SetQuest(QuestStatus quest)
    {
        _uIQuestInformationPanel.Setup(quest);
    }

    private void DestroyPanelChildObjects(Transform panel)
    {
        foreach (Transform gameobject in panel)
        {
            Destroy(gameobject.gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuestLogPanel();
        }
    }

    public void ToggleQuestLogPanel()
    {
        if (_questLogPanel.activeSelf == false)
        {
            ActivateQuestPanel();
        }
        else
        {
            DeativateQuestPanel();
        }
    }

    private void ActivateQuestPanel()
    {
        _questLogPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SetQuestInfoPanel();
    }

    private void DeativateQuestPanel()
    {
        DestroyPanelChildObjects(_questPickerPanel);
        _questLogPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
