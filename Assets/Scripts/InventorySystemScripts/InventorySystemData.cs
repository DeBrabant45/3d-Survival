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
        private Action _updateHotbarCallback;

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
                    _updateHotbarCallback.Invoke();
                    return countLeft;
                }
            }
            countLeft = _playerStorage.AddItem(item.ID, countLeft, item.IsStackable, item.StackLimit);
            if(countLeft > 0)
            {
                countLeft = _playerStorage.AddItem(item.ID, countLeft, item.IsStackable, item.StackLimit);
                if (countLeft == 0)
                {
                    _updateHotbarCallback.Invoke();
                    return countLeft;
                }
            }
            return countLeft;
        }

        public List<ItemData> GetItemsDataForInventory()
        {
            return _playerStorage.GetItemsData();
        }
    }
}
