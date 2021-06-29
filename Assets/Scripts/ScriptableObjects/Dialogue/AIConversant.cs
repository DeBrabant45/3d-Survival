using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AD.Dialogue
{
    public class AIConversant : MonoBehaviour, IUsable
    {
        [SerializeReference] Dialogue _dialogue = null;
        [SerializeField] string _aIName;
        private PlayerConversant _playerConversant;

        public string AIName { get => _aIName; }

        private void Start()
        {
            _playerConversant = FindObjectOfType<PlayerConversant>();
        }

        public void Use()
        {
            if(_dialogue == null)
            {
                return;
            }
            _playerConversant.StartDialogue(this, _dialogue);
        }
    }
}