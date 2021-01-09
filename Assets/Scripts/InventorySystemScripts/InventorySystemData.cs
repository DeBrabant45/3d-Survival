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
        public int SelectedItemUIID { get => _selectedItemUIID; }

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

        public List<ItemData> GetItemDataForHotbar()
        {
            return _hotbarStorage.GetItemsData();
        }

        public void RemoveItemFromInventory(int ui_id)
        {
            if(_hotbarUIElementIDList.Contains(ui_id))
            {
                _hotbarStorage.RemoveItemOfIndex(_hotbarUIElementIDList.IndexOf(ui_id));
            }
            else if(_inventoryUIElementIDList.Contains(ui_id))
            {
                _playerStorage.RemoveItemOfIndex(_inventoryUIElementIDList.IndexOf(ui_id));
            }
            else
            {
                throw new Exception("No Item with id " + ui_id);
            }
            ResetSelectedItem();
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
            //Adds item to hotbar as long as the hotbar contains item and the max count isn't exceeded.
            if (_hotbarStorage.CheckIfStorageContains(item.ID) && _hotbarStorage.CheckIfStorageHasEnoughOfItemCountToAdd(item.ID, countLeft) == true)
            {
                countLeft = _hotbarStorage.AddItem(item);
                UpdateHotbarCallback.Invoke();
                if (countLeft == 0)
                {
                    return countLeft;
                }
            }
            //Adds item to the inventory 
            countLeft = _playerStorage.AddItem(item.ID, countLeft, item.IsStackable, item.StackLimit);
            if (countLeft > 0)
            {
                countLeft = _playerStorage.AddItem(item.ID, countLeft, item.IsStackable, item.StackLimit);
                UpdateHotbarCallback.Invoke();
                if (countLeft == 0)
                {
                    return countLeft;
                }
            }
            return countLeft;
        }

        public int TakeOneFromItem(string id, int count)
        {
            int tempCount = 0;
            tempCount += TakeFromStorage(_hotbarStorage, id, count);
            if(tempCount == count)
            {
                return count;
            }
            else
            {
                tempCount += TakeFromStorage(_playerStorage, id, count - tempCount);
            }
            return tempCount;
        }

        private int TakeFromStorage(Storage storage, string id, int count)
        {
            var tempQty = storage.GetItemCount(id);
            if(tempQty > 0)
            {
                if(tempQty >= count)
                {
                    storage.TakeItemFromStorageIfContaintEnough(id, count);
                    return count;
                }
                else
                {
                    storage.TakeItemFromStorageIfContaintEnough(id, tempQty);
                    return tempQty;
                }
            }
            return 0;
        }

        public bool IsItemInStorage(string id, int count)
        {
            int qty = _playerStorage.GetItemCount(id);
            qty += _hotbarStorage.GetItemCount(id);
            if(qty >= count)
            {
                return true;
            }
            return false;
        }

        public bool IsInventoryFull()
        {
            return _hotbarStorage.CheckIfStorageIsFull() && _playerStorage.CheckIfStorageIsFull();
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

        private bool IsItemForUIInventoryStorageNotEmpty(int ui_id)
        {
            return _inventoryUIElementIDList.Contains(ui_id) && _playerStorage.CheckIfItemIsEmpty
                (_inventoryUIElementIDList.IndexOf(ui_id)) == false;
        }

        private bool IsItemForUIHotBarNotEmpty(int ui_id)
        {
            return _hotbarUIElementIDList.Contains(ui_id) && _hotbarStorage.CheckIfItemIsEmpty(_hotbarUIElementIDList.IndexOf(ui_id)) == false;
        }

        public bool IsSelectedItemEmpty(int ui_id)
        {
            if (IsItemForUIInventoryStorageNotEmpty(ui_id))
            {
                return _playerStorage.CheckIfItemIsEmpty(_inventoryUIElementIDList.IndexOf(ui_id));
            }
            else if (IsItemForUIHotBarNotEmpty(ui_id))
            {
                return _hotbarStorage.CheckIfItemIsEmpty(_hotbarUIElementIDList.IndexOf(ui_id));
            }
            else
            {
                return true;
            }
        }

        public string GetItemIDFor(int ui_id)
        {
            if(IsItemForUIInventoryStorageNotEmpty(ui_id))
            {
                return _playerStorage.GetIdOfItemWithIndex(_inventoryUIElementIDList.IndexOf(ui_id));
            }
            else if(IsItemForUIHotBarNotEmpty(ui_id))
            {
                return _hotbarStorage.GetIdOfItemWithIndex(_hotbarUIElementIDList.IndexOf(ui_id));
            }
            return null;
        }

        public int GetItemCountFor(int ui_id)
        {
            if (IsItemForUIInventoryStorageNotEmpty(ui_id))
            {
                return _playerStorage.GetCountOfItemWithIndex(_inventoryUIElementIDList.IndexOf(ui_id));
            }
            else if (IsItemForUIHotBarNotEmpty(ui_id))
            {
                return _hotbarStorage.GetCountOfItemWithIndex(_hotbarUIElementIDList.IndexOf(ui_id));
            }
            return -1;
        }

        public void TakeOneFromItem(int ui_id)
        {
            if (IsItemForUIInventoryStorageNotEmpty(ui_id))
            {
                _playerStorage.TakeFromItemWith(_inventoryUIElementIDList.IndexOf(ui_id), 1);
            }
            else if (IsItemForUIHotBarNotEmpty(ui_id))
            {
               _hotbarStorage.TakeFromItemWith(_hotbarUIElementIDList.IndexOf(ui_id), 1);
            }
            else
            {
                throw new Exception("No item with ui id " + ui_id);
            }
        }

        public SavedItemSystemData GetDataToSave()
        {
            return new SavedItemSystemData
            {
                PlayerStorageItems = _playerStorage.GetDataToSave(),
                HotbarStorageItems = _hotbarStorage.GetDataToSave(),
                PlayerStorageSize = _playerStorage.StorageLimit
            };
        }

        public void LoadData(SavedItemSystemData dataToLoad)
        {
            _playerStorage = new Storage(dataToLoad.PlayerStorageSize);
            //Clear potential old hotbar data before loading saved data into hotbar
            _hotbarStorage.ClearStorage();
            //Loads from the user's inventory
            LoadDataFromItemStorageList(dataToLoad.PlayerStorageItems, _playerStorage);
            //Loads from the user's hotbar
            LoadDataFromItemStorageList(dataToLoad.HotbarStorageItems, _hotbarStorage);
            UpdateHotbarCallback.Invoke();
        }

        private void LoadDataFromItemStorageList(List<ItemData> dataToLoad, Storage storage)
        {
            foreach (var item in dataToLoad)
            {
                if (item.IsNull == false)
                {
                    storage.SwapItemWithIndexFor(item.StorageIndex, item);
                }
            }
        }
    }
}
