using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryGeneralPanel;
    [SerializeField] private UIStorageButtons _uIStorageButtons;
    [SerializeField] private Canvas _canvas;

    public bool IsInventoryVisable { get => _inventoryGeneralPanel.activeSelf; }
    public Canvas Canvas { get => _canvas; set => _canvas = value; }

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
        _uIStorageButtons.HideAllButtons();
    }

    public void AssignUseButtonHandler(Action handler)
    {
        _uIStorageButtons.AssignUseButtonAction(handler);
    }    
    
    public void AssignDropButtonHandler(Action handler)
    {
        _uIStorageButtons.AssignDropButtonAction(handler);
    }

    public void ToggleItemButtons(bool useBtn, bool dropBtn)
    {
        _uIStorageButtons.ToggleDropButton(dropBtn);
        _uIStorageButtons.ToggleUseButton(useBtn);
    }
}
