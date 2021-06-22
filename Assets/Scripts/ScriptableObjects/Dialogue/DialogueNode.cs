using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        public string Text;
        public List<string> ChildernUniqueID = new List<string>();
        public Rect RectPosition = new Rect(0, 0, 200, 200);
    }
}