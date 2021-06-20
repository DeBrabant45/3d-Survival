using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private List<DialogueNode> _nodes = new List<DialogueNode>();
        private Dictionary<string, DialogueNode> _nodeLookUp = new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR
        private void Awake()
        {
            if(_nodes.Count == 0)
            {
                var rootNode = new DialogueNode();
                rootNode.UniqueID = Guid.NewGuid().ToString();
                _nodes.Add(rootNode);
            }
            OnValidate();
        }
#endif

        private void OnValidate()
        {
            _nodeLookUp.Clear();
            foreach (var node in GetAllNodes())
            {
                _nodeLookUp[node.UniqueID] = node;
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return _nodes;
        }

        public DialogueNode GetRootNode()
        {
            return _nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildern(DialogueNode parentNode)
        {
            if(parentNode.ChildernUniqueID != null)
            {
                foreach (var childID in parentNode.ChildernUniqueID)
                {
                    if (_nodeLookUp.ContainsKey(childID))
                    {
                        yield return _nodeLookUp[childID];
                    }
                }
            }
        }

        public void CreateNode(DialogueNode parent)
        {
            var newNode = new DialogueNode();
            newNode.UniqueID = Guid.NewGuid().ToString();
            parent.ChildernUniqueID.Add(newNode.UniqueID);
            _nodes.Add(newNode);
            OnValidate();
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            _nodes.Remove(nodeToDelete);
            OnValidate();
            RemoveLeftOverChildNodes(nodeToDelete);
        }

        private void RemoveLeftOverChildNodes(DialogueNode nodeToDelete)
        {
            foreach (var node in GetAllNodes())
            {
                node.ChildernUniqueID.Remove(nodeToDelete.UniqueID);
            }
        }
    }
}
