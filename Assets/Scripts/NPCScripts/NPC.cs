using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IUsable
{
    [SerializeField] protected string _characterName;
    [SerializeField] protected string[] _introduceDialogue;
    [SerializeField] protected string[] _returingDialogue;
    [SerializeField] protected DialogueSystem _dialogueSystem;
    protected bool IsFirstTimeMeeting = true;

    public virtual void Use()
    {
        if(IsFirstTimeMeeting != false)
        {
            _dialogueSystem.AddNewDialogue(_introduceDialogue, _characterName);
            IsFirstTimeMeeting = false;
        }
        else
        {
            _dialogueSystem.AddNewDialogue(_returingDialogue, _characterName);
        }
    }
}
