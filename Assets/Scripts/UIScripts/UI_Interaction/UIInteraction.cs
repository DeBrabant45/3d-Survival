using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UIScripts.UI_Interaction
{
    public class UIInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject _interactionPrefab;
        [SerializeField] private Transform _interactionPanel;

        // Use this for initialization
        void Start()
        {
            InventoryEvents.Instance.OnItemCollected += DisplayCollectedItem;
            foreach(Transform child in _interactionPanel)
            {
                Destroy(child.gameObject);
            }
        }

        private void DisplayCollectedItem(ItemSO item, int amount)
        {
            var panelGameObject = Instantiate(_interactionPrefab, _interactionPanel);
            foreach(Transform child in panelGameObject.transform)
            {
                var childImage = child.GetComponentInChildren<Image>();
                var childText = child.GetComponentInChildren<Text>();
                if (childImage != null)
                {
                    childImage.sprite = item.ImageSprite;
                }
                if(childText != null)
                {
                    childText.text = amount + "";
                }
            }
            Destroy(panelGameObject, 3f);
        }
    }
}