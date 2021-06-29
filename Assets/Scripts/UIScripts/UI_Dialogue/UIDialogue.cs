using UnityEngine;
using UnityEngine.UI;
using AD.Dialogue;
using System;

public class UIDialogue : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private Text _conversantNameText;
    [SerializeField] private Text _dialogueText;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _exitBtn;
    [SerializeField] private GameObject _aIResponsePanel;
    [SerializeField] private Transform _choicePanel;
    [SerializeField] private GameObject _dialogueChoiceBtnPrefab;
    [SerializeField] private PlayerConversant _playerConversant;

    private void Start()
    {
        if (_playerConversant == null)
        {
            _playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
        }
        _playerConversant.OnConversationUpdated += UpdateUI;
        _continueBtn.onClick.AddListener(() => _playerConversant.Next());
        _exitBtn.onClick.AddListener(() => _playerConversant.Exit());
        UpdateUI();
    }

    private void UpdateUI()
    {
        _dialoguePanel.SetActive(_playerConversant.IsActive());
        if (!_playerConversant.IsActive())
        {
            return;
        }
        _conversantNameText.text = _playerConversant.GetCurrentCoversantName();
        DestoryAllChoicePanelChildren();
        EnableAIResponse();
        EnablePlayerChoice();
    }

    private void DestoryAllChoicePanelChildren()
    {
        foreach (Transform child in _choicePanel)
        {
            Destroy(child.gameObject);
        }
    }

    private void EnableAIResponse()
    {
        if (_playerConversant.IsChoosing() == false)
        {
            _aIResponsePanel.SetActive(true);
            AddAIDialogue();
        }
        else
        {
            if(_aIResponsePanel.activeSelf)
            {
                _aIResponsePanel.SetActive(false);
            }
        }
    }

    private void AddAIDialogue()
    {
        _dialogueText.text = _playerConversant.GetText();
        _continueBtn.gameObject.SetActive(_playerConversant.HasNext());
    }

    private void EnablePlayerChoice()
    {
        if (_playerConversant.IsChoosing())
        {
            _choicePanel.gameObject.SetActive(true);
            AddPlayerChoices();
        }
        else
        {
            if(_choicePanel.gameObject.activeSelf)
            {
                _choicePanel.gameObject.SetActive(false);
            }
        }
    }

    private void AddPlayerChoices()
    {
        if(_playerConversant.IsChoosing())
        {
            foreach (var choiceNode in _playerConversant.GetChoices())
            {
                var newChoiceBtn = Instantiate<GameObject>(_dialogueChoiceBtnPrefab, _choicePanel);
                var newChoiceBtnTxt = newChoiceBtn.GetComponentInChildren<Text>();
                var button = newChoiceBtn.GetComponentInChildren<Button>();
                if (newChoiceBtnTxt != null)
                {
                    newChoiceBtnTxt.text = choiceNode.Text;
                }
                if(button != null)
                {
                    button.onClick.AddListener(() => 
                    {
                        _playerConversant.SelectChoice(choiceNode);
                    });
                }
            }
        }
    }
}
