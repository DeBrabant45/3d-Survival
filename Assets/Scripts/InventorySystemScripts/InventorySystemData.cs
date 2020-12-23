using SVS.InventorySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventorySystemData
    {
        [SerializeField] private int _selectedItemUIID = -1;
        private Storage _playerStorage;
        private Storage _hotbarStorage;
        private List<int> _inventoryUIElementIDList = new List<int>();
        private List<int> _hotbarUIElementIDList = new List<int>();


        public Action UpdateHotbarCallback;
        public int PlayerStorageLimit { get => _playerStorage.StorageLimit; }
        public int HotbarStorageLimit { get => _hotbarStorage.StorageLimit; }

        public InventorySystemData(int playerStorageSize, int hotbarStorageSize)
        {
            _playerStorage = new Storage(playerStorageSize);
            _hotbarStorage = new Storage(hotbarStorageSize);
        }

        public void SetSelectedItem(int ui_id)
        {
            _selectedItemUIID = ui_id;
        }

        public void ResetSelectedItem()
        {
            _selectedItemUIID = -1;
        }

        public void AddHotbarUIElement(int ui_id)
        {
            _hotbarUIElementIDList.Add(ui_id);
        }

        public void AddInventoryUIElement(int ui_id)
        {
            _inventoryUIElementIDList.Add(ui_id);
        }

        public void ClearInventoryUIElements()
        {
            _inventoryUIElementIDList.Clear();
        }

        public int AddToStorage(IInventoryItem item)
        {
            int countLeft = item.Count;
            if(_hotbarStorage.CheckIfStorageContains(item.ID))
            {
                countLeft = _hotbarStorage.AddItem(item);
                if(countLeft == 0)
                {
                    UpdateHotbarCallback.Invoke();
                    return countLeft;
                }
            }
            countLeft = _playerStorage.AddItem(item.ID, countLeft, item.IsStackable, item.StackLimit);
            if(countLeft > 0)
            {
                countLeft = _playerStorage.AddItem(item.ID, countLeft, item.IsStackable, item.StackLimit);
                if (countLeft == 0)
                {
                    UpdateHotbarCallback.Invoke();
                    return countLeft;
                }
            }
            return countLeft;
        }

        public List<ItemData> GetItemsDataForInventory()
        {
            return _playerStorage.GetItemsData();
        }

        public void SwapStorageItemsInsideInventory(int droppedItemID, int draggedItemID)
        {
            var storage_Id_DraggedItem = _inventoryUIElementIDList.IndexOf(draggedItemID);
            var storageData_Id_DraggedItem = _playerStorage.GetItemData(storage_Id_DraggedItem);
            var storage_Id_DroppedItem = _inventoryUIElementIDList.IndexOf(droppedItemID);

            if (IsItemForUIInventoryStorageNotEmpty(droppedItemID))
            {
                var storageData_Id_DroppedItem = _playerStorage.GetItemData(storage_Id_DroppedItem);
                //Swap data with dragged item to dropped item
                _playerStorage.SwapItemWithIndexFor(storage_Id_DraggedItem, storageData_Id_DroppedItem);
                //Swap data with dropped item to dragged item
                _playerStorage.SwapItemWithIndexFor(storage_Id_DroppedItem, storageData_Id_DraggedItem);
            }
            else
            {
                //Swap data with empty
                _playerStorage.SwapItemWithIndexFor(storage_Id_DroppedItem, storageData_Id_DraggedItem);
                _playerStorage.RemoveItemOfIndex(storage_Id_DraggedItem);
            }
        }

        private bool IsItemForUIInventoryStorageNotEmpty(int ui_id)
        {
            return _inventoryUIElementIDList.Contains(ui_id) && _playerStorage.CheckIfItemIsEmpty
                (_inventoryUIElementIDList.IndexOf(ui_id)) == false;
        }

        public void SwapStorageItemsInsideHotbar(int droppedItemID, int draggedItemID)
        {
            var storage_Id_DraggedItem = _hotbarUIElementIDList.IndexOf(draggedItemID);
            var storageData_Id_DraggedItem = _hotbarStorage.GetItemData(storage_Id_DraggedItem);
            var storage_Id_DroppedItem = _hotbarUIElementIDList.IndexOf(droppedItemID);

            if (IsItemForUIHotBarNotEmpty(droppedItemID))
            {
                var storageData_Id_DroppedItem = _hotbarStorage.GetItemData(storage_Id_DroppedItem);
                //Swap data with dragged item to dropped item
                _hotbarStorage.SwapItemWithIndexFor(storage_Id_DraggedItem, storageData_Id_DroppedItem);
                //Swap data with dropped item to dragged item
                _hotbarStorage.SwapItemWithIndexFor(storage_Id_DroppedItem, storageData_Id_DraggedItem);
            }
            else
            {
                //Swap data with empty
                _hotbarStorage.SwapItemWithIndexFor(storage_Id_DroppedItem, storageData_Id_DraggedItem);
                _hotbarStorage.RemoveItemOfIndex(storage_Id_DraggedItem);
            }
        }

        public void SwapStorageItemFromHotbarToInventory(int droppedItemID, int draggedItemID)
        {
            var storage_Id_DraggedItem = _hotbarUIElementIDList.IndexOf(draggedItemID);
            var storageData_Id_DraggedItem = _hotbarStorage.GetItemData(storage_Id_DraggedItem);
            var storage_Id_DroppedItem = _inventoryUIElementIDList.IndexOf(droppedItemID);

            if (IsItemForUIInventoryStorageNotEmpty(droppedItemID))
            {
                var storageData_Id_DroppedItem = _playerStorage.GetItemData(storage_Id_DroppedItem);
                //Swap data with dragged item to dropped item
                _hotbarStorage.SwapItemWithIndexFor(storage_Id_DraggedItem, storageData_Id_DroppedItem);
                //Swap data with dropped item to dragged item
                _playerStorage.SwapItemWithIndexFor(storage_Id_DroppedItem, storageData_Id_DraggedItem);
            }
            else
            {
                //Swap data with empty
                _playerStorage.SwapItemWithIndexFor(storage_Id_DroppedItem, storageData_Id_DraggedItem);
                _hotbarStorage.RemoveItemOfIndex(storage_Id_DraggedItem);
            }
        }

        private bool IsItemForUIHotBarNotEmpty(int ui_id)
        {
            return _hotbarStorage.CheckIfItemIsEmpty(_hotbarUIElementIDList.IndexOf(ui_id)) == false;
        }

        public void SwapStorageItemFromInventoryToHotbar(int droppedItemID, int draggedItemID)
        {
            var storage_Id_DraggedItem = _inventoryUIElementIDList.IndexOf(draggedItemID);
            var storageData_Id_DraggedItem = _playerStorage.GetItemData(storage_Id_DraggedItem);
            var storage_Id_DroppedItem = _hotbarUIElementIDList.IndexOf(droppedItemID);

            if (IsItemForUIHotBarNotEmpty(droppedItemID))
            {
                var storageData_Id_DroppedItem = _hotbarStorage.GetItemData(storage_Id_DroppedItem);
                //Swap data with dragged item to dropped item
                _playerStorage.SwapItemWithIndexFor(storage_Id_DraggedItem, storageData_Id_DroppedItem);
                //Swap data with dropped item to dragged item
                _hotbarStorage.SwapItemWithIndexFor(storage_Id_DroppedItem, storageData_Id_DraggedItem);
                Debug.Log(storageData_Id_DroppedItem);
            }
            else
            {
                //Swap data with empty
                _hotbarStorage.SwapItemWithIndexFor(storage_Id_DroppedItem, storageData_Id_DraggedItem);
                _playerStorage.RemoveItemOfIndex(storage_Id_DraggedItem);
            }
        }
    }
}
