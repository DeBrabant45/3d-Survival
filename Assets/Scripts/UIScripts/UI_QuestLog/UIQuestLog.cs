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
    [SerializeField] private GameObject _completedQuestCheckMark;

    [SerializeField] private Transform _questPickerPanel;

    [SerializeField] private Button _activeQuestBtn;
    [SerializeField] private Button _completedQuestsBtn;
    [SerializeField] private Button _questPickerBtnPrefab;

    [SerializeField] private Text _questTitle;
    [SerializeField] private Text _questGiverName;
    [SerializeField] private Text _questDescription;
    [SerializeField] private Text _completedQuestTxt;

    void Start()
    {
        _questLogPanel.SetActive(false);
        _completedQuestCheckMark.SetActive(false);
        _activeQuestBtn.onClick.AddListener(() => SetQuestInfoPanel(_activeQuests));
        _completedQuestsBtn.onClick.AddListener(() => SetQuestInfoPanel(_completedQuests));
    }
    
    private void SetQuestInfoPanel(GameObject questPanel)
    {
        DestroyPanelChildObjects();
        var quests = questPanel.GetComponents<Quest>();
        if(quests.Length > 0)
        {
            SetQuest(quests[0]);
            CreateButtons(quests);
        }
        else
        {
            SetQuest(null);
        }
    }

    private void CreateButtons(Quest[] quests)
    {
        foreach (var quest in quests)
        {
            var btn = _questPickerBtnPrefab;
            btn.GetComponentInChildren<Text>().text = quest.Title;
            var newBtn = Instantiate(btn, _questPickerPanel);
            newBtn.onClick.AddListener(() => SetQuest(quest));
        }
    }

    private void SetQuest(Quest quest)
    {
        if (quest != null)
        {
            _questTitle.text = quest.Title;
            _questGiverName.text = quest.QuestGiverName;
            _questDescription.text = quest.Description;
        }
        else
        {
            _questTitle.text = "";
            _questGiverName.text = "";
            _questDescription.text = "";
        }
        SetCompletedQuestInfo(quest);
    }

    private void SetCompletedQuestInfo(Quest quest)
    {
        if(quest != null && quest.IsCompleted)
        {
            _questDescription.color = Color.grey;
            _completedQuestCheckMark.SetActive(true);
            _completedQuestTxt.text = quest.OnceCompletedInfo;
        }
        else
        {
            _questDescription.color = Color.black;
            _completedQuestCheckMark.SetActive(false);
        }
    }

    private void DestroyPanelChildObjects()
    {
        foreach (Transform questBtn in _questPickerPanel)
        {
            Destroy(questBtn.gameObject);
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
        SetQuestInfoPanel(_activeQuests);
    }

    private void DeativateQuestPanel()
    {
        DestroyPanelChildObjects();
        _questLogPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
