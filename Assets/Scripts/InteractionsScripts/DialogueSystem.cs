using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    private string _npcName;
    private List<string> _dialogueLines = new List<string>();
    private int _dialogueIndex;
    private Action _onDialogueTrigger;

    public List<string> DialogueLines { get => _dialogueLines; }
    public string NPCName { get => _npcName; }
    public int DialogueIndex { get => _dialogueIndex; set => _dialogueIndex = value; }
    public Action OnDialogueTrigger { get => _onDialogueTrigger; set => _onDialogueTrigger = value; }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        _dialogueIndex = 0;
        _dialogueLines = new List<string>();
        _npcName = npcName;
        foreach (string line in lines)
        {
            _dialogueLines.Add(line);
        }
        _onDialogueTrigger?.Invoke();
        Debug.Log(_dialogueLines.Count);
    }
}
