using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using System;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int _playerStorageSize = 20;
    private UIInventory _uIInventory;
    private InventorySystemData _inventoryData;

    public int PlayerStorageSize { get => _playerStorageSize; }

    private void Awake()
    {
        _uIInventory = GetComponent<UIInventory>();    
    }

    private void Start()
    {
        _inventoryData = new InventorySystemData(_playerStorageSize, _uIInventory.HotbarElementsCount);
        var hotbarUIElements = _uIInventory.GetUIElementsForHotbar();
        for (int i = 0; i < hotbarUIElements.Count; i++)
        {
            _inventoryData.AddHotbarUIElement(hotbarUIElements[i].GetInstanceID());
            hotbarUIElements[i].OnClickEvent += UseHotBarItemHandler;
        }
    }

    private void UseHotBarItemHandler(int ui_id, bool isEmpty)
    {
        if(isEmpty)
        {
            return;
        }
        Debug.Log("Using hbar item");
    }

    public void ToggleInventory()
    {
        if(_uIInventory.IsInventoryVisable == false)
        {
            _inventoryData.ResetSelectedItem();
            _inventoryData.ClearInventoryUIElements();
            PrepareUI();
            PutDataInUI();
        }
        _uIInventory.ToggleUI();
    }

    private void PutDataInUI()
    {
        return;
    }

    private void PrepareUI()
    {
        _uIInventory.PrepareInventoryItems(_inventoryData.PlayerStorageLimit);
        AddEventHandlersToInventoryUIElements();
    }

    private void AddEventHandlersToInventoryUIElements()
    {
        foreach(var uIItemElement in _uIInventory.GetUIElementsForInventory())
        {
            uIItemElement.OnClickEvent += UIElementSelectedHandler;
        }
    }

    private void UIElementSelectedHandler(int ui_id, bool isEmpty)
    {
        if (isEmpty == false)
        {
            _inventoryData.ResetSelectedItem();
            _inventoryData.SetSelectedItem(ui_id);
            Debug.Log("Selecting invt item");
        }
        return;
    }
}
