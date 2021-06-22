using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<DialogueNode> _nodes = new List<DialogueNode>();
        private Dictionary<string, DialogueNode> _nodeLookUp = new Dictionary<string, DialogueNode>();

        private void OnValidate()
        {
            _nodeLookUp.Clear();
            foreach (var node in GetAllNodes())
            {
                _nodeLookUp[node.name] = node;
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
            var newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            Undo.RegisterCreatedObjectUndo(newNode, "Create Dialogue Node");
            if (parent != null)
            {
                parent.ChildernUniqueID.Add(newNode.name);
            }
            _nodes.Add(newNode);
            OnValidate();
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            _nodes.Remove(nodeToDelete);
            OnValidate();
            RemoveLeftOverChildNodes(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private void RemoveLeftOverChildNodes(DialogueNode nodeToDelete)
        {
            foreach (var node in GetAllNodes())
            {
                node.ChildernUniqueID.Remove(nodeToDelete.name);
            }
        }

        public void OnBeforeSerialize()
        {
            if (_nodes.Count == 0)
            {
                CreateNode(null);
            }
            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (DialogueNode node in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
        }

        public void OnAfterDeserialize()
        {
        }
    }
}
