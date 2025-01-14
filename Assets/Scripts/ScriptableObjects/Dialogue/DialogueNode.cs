﻿using AD.General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AD.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] private bool _isPlayerSpeaking = false;
        [SerializeField] private string _text;
        [SerializeField] private List<string> _children = new List<string>();
        [SerializeField] private Rect _rect = new Rect(0, 0, 200, 100);
        [SerializeField] private string _onEnterAction;
        [SerializeField] private string _onExitAction;
        [SerializeField] private DialogueObjective _dialogueObjective;
        [SerializeField] private Condition _condition;

        public string Text { get => _text; }
        public List<string> Children { get => _children; }
        public Rect RectPosition { get => _rect; }
        public bool IsPlayerSpeaking { get => _isPlayerSpeaking; }
        public string OnEnterAction { get => _onEnterAction; }
        public string OnExitAction { get => _onExitAction; }
        public DialogueObjective DialogueObjective { get => _dialogueObjective; }

        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return _condition.Check(evaluators);
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            _rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if(newText != _text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                _text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            _children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            _children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public void SetIsPlayerSpeaking(bool isPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            _isPlayerSpeaking = isPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}