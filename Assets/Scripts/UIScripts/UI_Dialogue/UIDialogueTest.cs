using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueTest : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Text _dialogueText;
    [SerializeField] private Text _nameText;
    [SerializeField] private DialogueSystem _dialogueSystem;

    private void Awake()
    {
        _dialoguePanel.SetActive(false);
        _continueBtn.onClick.AddListener(HandleContiune);
        _dialogueSystem.OnDialogueTrigger += ActivateDialoguePanel;
    }

    private void ActivateDialoguePanel()
    {
        _dialoguePanel.SetActive(true);
        _nameText.text = _dialogueSystem.NPCName;
        _dialogueText.text = _dialogueSystem.DialogueLines[_dialogueSystem.DialogueIndex];
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void HandleContiune()
    {
        if(_dialogueSystem.DialogueIndex < _dialogueSystem.DialogueLines.Count-1)
        {
            _dialogueSystem.DialogueIndex++;
            _dialogueText.text = _dialogueSystem.DialogueLines[_dialogueSystem.DialogueIndex];
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _dialoguePanel.SetActive(false);
        }
    }
}
