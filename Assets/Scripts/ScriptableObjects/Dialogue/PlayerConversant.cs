using AD.General;
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
        [SerializeField] private List<GameObject> _predicateGameObjects;

        private Dialogue _currentDialogue;
        private DialogueNode _currentNode = null;
        private AIConversant _currentConversant = null;
        private bool _isChoosing = false;
        private List<IPredicateEvaluator> _predicateEvaluator = new List<IPredicateEvaluator>();

        public event Action OnConversationUpdated;

        void Start()
        {
            foreach (var item in _predicateGameObjects)
            {
                var predicate = item.GetComponent<IPredicateEvaluator>();
                if (predicate != null)
                {
                    _predicateEvaluator.Add(predicate);
                }
            }
        }

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
            TriggerDialogueObjective();
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
            return FilterOnCondition(_currentDialogue.GetPlayerOnlyChildren(_currentNode));
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
            var numberPlayerResponses = FilterOnCondition(_currentDialogue.GetPlayerOnlyChildren(_currentNode)).Count();
            if (numberPlayerResponses > 0)
            {
                _isChoosing = true;
                TriggerExitAction();
                TriggerDialogueObjective();
                OnConversationUpdated?.Invoke();
                return;
            }
            DialogueNode[] children = FilterOnCondition(_currentDialogue.GetAIOnlyChildren(_currentNode)).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Length);
            TriggerExitAction();
            TriggerDialogueObjective();
            if (children.Length > 0)
            {
                _currentNode = children[randomIndex];
            }
            TriggerEnterAction();
            OnConversationUpdated?.Invoke();
        }

        public bool HasNext()
        {
            return FilterOnCondition(_currentDialogue.GetAllChildern(_currentNode)).Count() > 0;
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (var node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return _predicateEvaluator;
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

        private void TriggerDialogueObjective()
        {
            if(_currentNode != null && _currentNode.DialogueObjective != null)
            {
                QuestEvents.Instance.CompleteDialogueObjective(_currentNode.DialogueObjective);
            }
        }

        private void TriggerAction(string action)
        {
            foreach (var trigger in _currentConversant.GetComponent<DialogueTriggerList>().DialogueTriggers)
            {
                trigger.TriggerEvent(action);
            }
        }
    }
}