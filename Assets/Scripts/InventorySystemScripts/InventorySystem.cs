using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using System;
using SVS.InventorySystem;
using UnityEngine.EventSystems;

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
        ItemData artificialItem = new ItemData(0, 10, "21780ff88a3d4166bc265904c53e402c", true, 100);
        AddToStorage(artificialItem);
        var hotbarUIElements = _uIInventory.GetUIElementsForHotbar();
        for (int i = 0; i < hotbarUIElements.Count; i++)
        {
            _inventoryData.AddHotbarUIElement(hotbarUIElements[i].GetInstanceID());
            hotbarUIElements[i].OnClickEvent += UseHotBarItemHandler;
        }
    }

    private void UseHotBarItemHandler(int ui_id, bool isEmpty)
    {
        Debug.Log("Using hbar item");
        if (isEmpty)
        {
            return;
        }
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
        var uIElements = _uIInventory.GetUIElementsForInventory();
        var inventoryItems = _inventoryData.GetItemsDataForInventory();
        for (int i = 0; i < uIElements.Count; i++)
        {
            var uIItemElement = uIElements[i];
            var itemData = inventoryItems[i];
            if(itemData.IsNull == false)
            {
                var itemName = ItemDataManager.Instance.GetItemName(itemData.ID);
                var itemSprite = ItemDataManager.Instance.GetItemSprite(itemData.ID);
                uIItemElement.SetInventoryUIElement(itemName, itemData.Count, itemSprite);
            }
            _inventoryData.AddInventoryUIElement(uIItemElement.GetInstanceID());
        }
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
            uIItemElement.DragStartCallBack += UIElementBeginDragHandler;
            uIItemElement.DragContinueCallBack += UIElementContinueDragHandler;
            uIItemElement.DragStopCallBack += UIElementStopDragHandler;
            uIItemElement.DropCallBack += UIElementDropHandler;
        }
    }

    private void UIElementDropHandler(PointerEventData eventData, int ui_id)
    {

    }

    private void UIElementStopDragHandler(PointerEventData eventData)
    {
        _uIInventory.DestroyDraggedObject();
    }

    private void UIElementContinueDragHandler(PointerEventData eventData)
    {
        _uIInventory.MoveDraggableItem(eventData);
    }

    private void UIElementBeginDragHandler(PointerEventData eventData, int ui_id)
    {
        _uIInventory.DestroyDraggedObject();
        _uIInventory.CreateDraggableItem(ui_id);
    }

    private void UIElementSelectedHandler(int ui_id, bool isEmpty)
    {
        Debug.Log("Selecting invt item");
        if (isEmpty == false)
        {
            _inventoryData.ResetSelectedItem();
            _inventoryData.SetSelectedItem(ui_id);
        }
        return;
    }

    public int AddToStorage(IInventoryItem item)
    {
        int value = _inventoryData.AddToStorage(item);
        return value;
    }
}
