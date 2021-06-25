using System.Linq;
using UnityEngine;

namespace AD.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] private Dialogue _currentDialogue;
        private DialogueNode _currentNode = null;

        private void Awake()
        {
            _currentNode = _currentDialogue.GetRootNode();
        }

        public string GetText()
        {
            if (_currentNode == null)
            {
                return "";
            }
            return _currentNode.Text;
        }

        public void Next()
        {
           DialogueNode[] children = _currentDialogue.GetAllChildern(_currentNode).ToArray();
            int randomIndex = Random.Range(0, children.Length);
            _currentNode = children[randomIndex];
        }

        public bool HasNext()
        {
            return _currentDialogue.GetAllChildern(_currentNode).Count() > 0;
        }

    }
}