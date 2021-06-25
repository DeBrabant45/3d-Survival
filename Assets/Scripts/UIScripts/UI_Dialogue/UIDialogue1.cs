using UnityEngine;
using UnityEngine.UI;
using AD.Dialogue;
using System;

public class UIDialogue1 : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _dialogueText;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _exitBtn;

    private PlayerConversant _playerConversant;

    private void Start()
    {
        _playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
        _continueBtn.onClick.AddListener(HandleContiune);
        _exitBtn.onClick.AddListener(HandleExit);
        UpdateUI();
    }

    private void HandleContiune()
    {
        _playerConversant.Next();
        UpdateUI();
    }

    private void HandleExit()
    {

    }

    private void UpdateUI()
    {
        _dialogueText.text = _playerConversant.GetText();
        _continueBtn.gameObject.SetActive(_playerConversant.HasNext());
    }
}
