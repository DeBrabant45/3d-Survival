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
        _inventoryData.UpdateHotbarCallback += UpdateHotBarHandler;
        ItemData artificialItem = new ItemData(0, 11, "21780ff88a3d4166bc265904c53e402c", true, 100);
        ItemData artificialItem1 = new ItemData(0, 90, "21780ff88a3d4166bc265904c53e402c", true, 100);
        AddToStorage(artificialItem);
        AddToStorage(artificialItem1);
        AddEventHandlersToHotbarUIElements();
    }

    private void UpdateHotBarHandler()
    {
        var uIElements = _uIInventory.GetUIElementsForHotbar();
        var hotbarItemList = _inventoryData.GetItemDataForHotbar();
        for (int i = 0; i < uIElements.Count; i++)
        {
            var uIItemElement = uIElements[i];
            uIItemElement.ClearItem();
            var itemData = hotbarItemList[i];
            if (itemData.IsNull == false)
            {
                var itemName = ItemDataManager.Instance.GetItemName(itemData.ID);
                var itemSprite = ItemDataManager.Instance.GetItemSprite(itemData.ID);
                uIItemElement.SetInventoryUIElement(itemName, itemData.Count, itemSprite);
            }
        }
    }

    private void AddEventHandlersToHotbarUIElements()
    {
        var hotbarUIElements = _uIInventory.GetUIElementsForHotbar();
        for (int i = 0; i < hotbarUIElements.Count; i++)
        {
            _inventoryData.AddHotbarUIElement(hotbarUIElements[i].GetInstanceID());
            hotbarUIElements[i].OnClickEvent += UseHotBarItemHandler;
            hotbarUIElements[i].DragStartCallBack += UIElementBeginDragHandler;
            hotbarUIElements[i].DragContinueCallBack += UIElementContinueDragHandler;
            hotbarUIElements[i].DragStopCallBack += UIElementStopDragHandler;
            hotbarUIElements[i].DropCallBack += UIElementDropHandler;
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
            DeselectCurrentItem();
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

    private void HandleUIItemFromHotbar(int droppedItemID, int draggedItemID)
    {
        if (_uIInventory.CheckItemInInventory(droppedItemID))
        {
            // item is swapping from hot bar to inventory
            DropItemsFromHotbarToInventory(droppedItemID, draggedItemID);
        }
        else
        {
            // item is swapping between hot bar to hot bar
            DropItemsFromHotbarToHotbar(droppedItemID, draggedItemID);
        }
    }

    private void DropItemsFromHotbarToHotbar(int droppedItemID, int draggedItemID)
    {
        _uIInventory.SwapUIHotbarItemToHotBarSlot(droppedItemID, draggedItemID);
        _inventoryData.SwapStorageItemsInsideHotbar(droppedItemID, draggedItemID);
    }

    private void DropItemsFromHotbarToInventory(int droppedItemID, int draggedItemID)
    {
        _uIInventory.SwapUIHotbarItemToInventorySlot(droppedItemID, draggedItemID);
        _inventoryData.SwapStorageItemFromHotbarToInventory(droppedItemID, draggedItemID);
    }


    private void HandleUIItemFromInventory(int droppedItemID, int draggedItemID)
    {
        if (_uIInventory.CheckItemInInventory(droppedItemID))
        {
            //item is from inventory
            DropItemsFromInventoryToInventory(droppedItemID, draggedItemID);
        }
        else
        {
            //item is from hotbar
            DropItemsFromInventoryToHotbar(droppedItemID, draggedItemID);
        }
    }

    private void DropItemsFromInventoryToHotbar(int droppedItemID, int draggedItemID)
    {
        _uIInventory.SwapUIInventoryItemToHotBarSlot(droppedItemID, draggedItemID);
        _inventoryData.SwapStorageItemFromInventoryToHotbar(droppedItemID, draggedItemID);
    }

    private void DropItemsFromInventoryToInventory(int droppedItemID, int draggedItemID)
    {
        _uIInventory.SwapUIInventoryItemToInventorySlot(droppedItemID, draggedItemID);
        _inventoryData.SwapStorageItemsInsideInventory(droppedItemID, draggedItemID);
    }

    private void UIElementDropHandler(PointerEventData eventData, int droppedItemID)
    {
        if(_uIInventory.DraggableItem != null)
        {
            var draggedItemID = _uIInventory.DraggableItemPanel.GetInstanceID();
            if (draggedItemID == droppedItemID)
                return;
            DeselectCurrentItem();
            if (_uIInventory.CheckItemInInventory(draggedItemID)) //if item is coming from the iventory to the hotbar
            {
                HandleUIItemFromInventory(droppedItemID, draggedItemID);

            }
            else //if item is coming from the hot bar to the inventory
            {
                HandleUIItemFromHotbar(droppedItemID, draggedItemID);
            }
        }
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
            DeselectCurrentItem();
            _inventoryData.SetSelectedItem(ui_id);
        }
        return;
    }

    private void DeselectCurrentItem()
    {
        _inventoryData.ResetSelectedItem();
    }

    public int AddToStorage(IInventoryItem item)
    {
        int value = _inventoryData.AddToStorage(item);
        return value;
    }
}
