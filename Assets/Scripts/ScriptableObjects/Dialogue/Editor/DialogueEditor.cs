using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        private Dialogue _selectedDialogue = null;
        [NonSerialized] private GUIStyle _nodeStyle = null;
        [NonSerialized] private DialogueNode _draggingNode = null;
        [NonSerialized] private Vector2 _draggingOffSet;
        [NonSerialized] private DialogueNode _createNode = null;
        [NonSerialized] private DialogueNode _deleteNode = null;
        [NonSerialized] private DialogueNode _linkParentNode = null;

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int id, int line)
        {
            var dialogue = EditorUtility.InstanceIDToObject(id) as Dialogue;
            if(dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
            SetStyleForNode();
        }

        private void SetStyleForNode()
        {
            _nodeStyle = new GUIStyle();
            _nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            _nodeStyle.normal.textColor = Color.white;
            _nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            _nodeStyle.border = new RectOffset(12, 12, 12, 12);
        }

        private void OnSelectionChanged()
        {
            var selectedDialogue = Selection.activeObject as Dialogue;
            if(selectedDialogue != null)
            {
                _selectedDialogue = selectedDialogue;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if(_selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected");
            }
            else
            {
                ProcessEvents();
                foreach (DialogueNode node in _selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);
                    DrawConnections(node);
                }
                if(_createNode != null)
                {
                    Undo.RecordObject(_selectedDialogue, "Added Dialogue Node");
                    _selectedDialogue.CreateNode(_createNode);
                    _createNode = null;
                }
                if(_deleteNode != null)
                {
                    Undo.RecordObject(_selectedDialogue, "Deleted Dialogue Node");
                    _selectedDialogue.DeleteNode(_deleteNode);
                    _deleteNode = null;
                }
            }
        }

        private void ProcessEvents()
        {
            if(Event.current.type == EventType.MouseDown && _draggingNode == null)
            {
                _draggingNode = GetNodeAtPoint(Event.current.mousePosition);
                if(_draggingNode != null)
                {
                    _draggingOffSet = _draggingNode.RectPosition.position - Event.current.mousePosition;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && _draggingNode != null)
            {
                Undo.RecordObject(_selectedDialogue, "Update Dialogue Rect Position");
                _draggingNode.RectPosition.position = Event.current.mousePosition + _draggingOffSet;
                GUI.changed = true;
            }
            else if(Event.current.type == EventType.MouseUp && _draggingNode != null)
            {
                _draggingNode = null;
            }
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            foreach (DialogueNode node in _selectedDialogue.GetAllNodes())
            {
                if(node.RectPosition.Contains(point))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }

        private void DrawNode(DialogueNode node)
        {
            GUILayout.BeginArea(node.RectPosition, _nodeStyle);
            EditorGUI.BeginChangeCheck();
            var newText = EditorGUILayout.TextField(node.Text);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_selectedDialogue, "Update Dialogue Text");
                node.Text = newText;
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                _createNode = node;
            }
            DrawLinkButtons(node);
            if (GUILayout.Button("-"))
            {
                _deleteNode = node;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        private void DrawLinkButtons(DialogueNode node)
        {
            if (_linkParentNode == null)
            {
                if (GUILayout.Button("link"))
                {
                    _linkParentNode = node;
                }
            }
            else if(_linkParentNode == node)
            {
                if (GUILayout.Button("cancel"))
                {
                    _linkParentNode = null;
                }
            }
            else if(_linkParentNode.ChildernUniqueID.Contains(node.UniqueID))
            {
                if (GUILayout.Button("unlink"))
                {
                    Undo.RecordObject(_selectedDialogue, "Remove Dialogue Link");
                    _linkParentNode.ChildernUniqueID.Remove(node.UniqueID);
                    _linkParentNode = null;
                }
            }
            else
            {
                if (GUILayout.Button("child"))
                {
                    Undo.RecordObject(_selectedDialogue, "Add Dialogue Link");
                    _linkParentNode.ChildernUniqueID.Add(node.UniqueID);
                    _linkParentNode = null;
                }
            }
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.RectPosition.xMax, node.RectPosition.center.y);
            foreach (DialogueNode childNode in _selectedDialogue.GetAllChildern(node))
            {
                Vector3 endPosition = new Vector2(childNode.RectPosition.xMin, childNode.RectPosition.center.y);
                Vector3 controlPointOffset = new Vector2(100, 0);
                //Vector3 controlPointOffset = endPosition - startPosition;
                //controlPointOffset.y = 0;
                //controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startPosition, endPosition,
                    startPosition + controlPointOffset,
                    endPosition - controlPointOffset,
                    Color.white, null, 4f);
            }
        }

    }
}
