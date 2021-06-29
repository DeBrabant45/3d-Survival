using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AD.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] private string _playerName;
        private Dialogue _currentDialogue;
        private DialogueNode _currentNode = null;
        private AIConversant _currentConversant = null;
        private bool _isChoosing = false;

        public event Action OnConversationUpdated;

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            _currentConversant = newConversant;
            _currentDialogue = newDialogue;
            _currentNode = _currentDialogue.GetRootNode();
            TriggerEnterAction();
            OnConversationUpdated?.Invoke();
        }

        public void Exit()
        {
            _currentDialogue = null;
            TriggerExitAction();
            _currentNode = null;
            _isChoosing = false;
            _currentConversant = null;
            OnConversationUpdated?.Invoke();
        }

        public bool IsActive()
        {
            return _currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return _isChoosing;
        }

        public string GetCurrentCoversantName()
        {
            if(_isChoosing)
            {
                return _playerName;
            }
            else
            {
                return _currentConversant.AIName;
            }
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return _currentDialogue.GetPlayerOnlyChildren(_currentNode);
        }

        public string GetText()
        {
            if (_currentNode == null)
            {
                return "";
            }
            return _currentNode.Text;
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            _currentNode = chosenNode;
            TriggerEnterAction();
            _isChoosing = false;
            Next();
        }

        public void Next()
        {
            var numberPlayerResponses = _currentDialogue.GetPlayerOnlyChildren(_currentNode).Count();
            if (numberPlayerResponses > 0)
            {
                _isChoosing = true;
                TriggerExitAction();
                OnConversationUpdated?.Invoke();
                return;
            }
            DialogueNode[] children = _currentDialogue.GetAIOnlyChildren(_currentNode).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Length);
            TriggerExitAction();
            _currentNode = children[randomIndex];
            TriggerEnterAction();
            OnConversationUpdated?.Invoke();
        }

        public bool HasNext()
        {
            return _currentDialogue.GetAllChildern(_currentNode).Count() > 0;
        }

        private void TriggerEnterAction()
        {
            if(_currentNode != null && _currentNode.OnEnterAction != "")
            {
                TriggerAction(_currentNode.OnEnterAction);
            }
        }

        private void TriggerExitAction()
        {
            if (_currentNode != null && _currentNode.OnExitAction != "")
            {
                TriggerAction(_currentNode.OnExitAction);
            }
        }

        private void TriggerAction(string action)
        {
            foreach (var trigger in _currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }
}