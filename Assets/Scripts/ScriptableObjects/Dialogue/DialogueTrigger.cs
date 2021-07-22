using System;
using UnityEngine;
using UnityEngine.Events;

namespace AD.Dialogue
{
    [Serializable]
    public class DialogueTrigger 
    {
        [SerializeField] private string _action;
        [SerializeField] private UnityEvent _onTrigger;

        //refactor 
        public void TriggerEvent(string actionToTrigger)
        {
            if (actionToTrigger == _action)
            {
                _onTrigger?.Invoke();
            }
        }
    }
}