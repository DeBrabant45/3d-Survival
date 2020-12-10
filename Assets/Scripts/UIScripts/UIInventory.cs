using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryGeneralPanel;

    public bool IsInventoryVisable { get => _inventoryGeneralPanel.activeSelf; }

    private void Awake()
    {
        _inventoryGeneralPanel.SetActive(false);
    }

    public void ToggleUI()
    {
        if(_inventoryGeneralPanel.activeSelf == false)
        {
            _inventoryGeneralPanel.SetActive(true);
        }
        else
        {
            _inventoryGeneralPanel.SetActive(false);
        }
    }
}
