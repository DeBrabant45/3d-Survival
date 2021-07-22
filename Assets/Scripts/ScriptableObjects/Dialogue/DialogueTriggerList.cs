using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AD.Dialogue
{
    public class DialogueTriggerList : MonoBehaviour
    {
        [SerializeField] private List<DialogueTrigger> _dialogueTriggers;

        public List<DialogueTrigger> DialogueTriggers { get => _dialogueTriggers; }
    }
}
