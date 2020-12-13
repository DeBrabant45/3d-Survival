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

    private void UseHotBarItemHandler(int arg1, bool arg2)
    {

    }

    public void ToggleInventory()
    {
        if(_uIInventory.IsInventoryVisable == false)
        {
            //populate inventory
        }
        _uIInventory.ToggleUI();
    }
}
