using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject _storagePanel;
    [SerializeField] private GameObject _storagePrefab;
    private Dictionary<int, InventoryItemPanel> _inventoryUIItems = new Dictionary<int, InventoryItemPanel>();

    public Dictionary<int, InventoryItemPanel> InventoryUIItems { get => _inventoryUIItems; set => _inventoryUIItems = value; }

    private InventoryItemPanel GetItemFromInventoryDicitionary(int ui_id)
    {
        Debug.Log("Item removed from the Inventory");
        return _inventoryUIItems[ui_id];
    }

    public void AddItemsToInventory(int playerStorageLimit)
    {
        for (int i = 0; i < playerStorageLimit; i++)
        {
            var element = Instantiate(_storagePrefab, Vector3.zero, Quaternion.identity, _storagePanel.transform);
            var itemHelper = element.GetComponent<InventoryItemPanel>();
            _inventoryUIItems.Add(itemHelper.GetInstanceID(), itemHelper);
        }
    }

    public void DestoryAllItemsInInventory(int playerStorageLimit)
    {
        for (int i = 0; i < playerStorageLimit; i++)
        {
            foreach (Transform child in _storagePanel.transform)
            {
                Destroy(child.gameObject);
            }
        }
        _inventoryUIItems.Clear();
    }

    public List<InventoryItemPanel> GetUIElementsForInventory()
    {
        return _inventoryUIItems.Values.ToList();
    }

    public bool IsItemInInventoryDictionary(int ui_id)
    {
        return _inventoryUIItems.ContainsKey(ui_id);
    }

    public void HighLightSelectedItem(int ui_id)
    {
        if(IsItemInInventoryDictionary(ui_id))
        {
            _inventoryUIItems[ui_id].ToggleHighLight(true);
        }
        else
        {
            return;
        }
    }

    public void DeHighLightSelectedItem(int ui_id)
    {
        if (IsItemInInventoryDictionary(ui_id))
        {
            _inventoryUIItems[ui_id].ToggleHighLight(false);
        }
        else
        {
            return;
        }
    }

    public void SwapUIInventoryItemToInventorySlot(int droppedItemID, int draggedItemID)
    {
        var tempName = _inventoryUIItems[draggedItemID].ItemName;
        var tempCount = _inventoryUIItems[draggedItemID].ItemCount;
        var tempSprite = _inventoryUIItems[draggedItemID].ItemImage.sprite;
        var tempIsEmpty = _inventoryUIItems[draggedItemID].IsEmpty;

        var droppedItemData = _inventoryUIItems[droppedItemID];
        //dragged Item
        _inventoryUIItems[draggedItemID].SwapWithData(droppedItemData.ItemName, droppedItemData.ItemCount, droppedItemData.ItemImage.sprite, droppedItemData.IsEmpty);
        //dropped item
        _inventoryUIItems[droppedItemID].SwapWithData(tempName, tempCount, tempSprite, tempIsEmpty);
    }

    public void SwapUIInventoryItemToHotBarSlot(Dictionary<int, InventoryItemPanel> hotbarItem, int droppedItemID, int draggedItemID)
    {
        var tempName = _inventoryUIItems[draggedItemID].ItemName;
        var tempCount = _inventoryUIItems[draggedItemID].ItemCount;
        var tempSprite = _inventoryUIItems[draggedItemID].ItemImage.sprite;
        var tempIsEmpty = _inventoryUIItems[draggedItemID].IsEmpty;
        var droppedItemData = hotbarItem[droppedItemID];

        _inventoryUIItems[draggedItemID].SwapWithData(droppedItemData.ItemName, droppedItemData.ItemCount, droppedItemData.ItemImage.sprite, droppedItemData.IsEmpty);
        hotbarItem[droppedItemID].SwapWithData(tempName, tempCount, tempSprite, tempIsEmpty);
    }

    public void UpdateItemInfo(int ui_id, int count)
    {
        if (IsItemInInventoryDictionary(ui_id) == true)
        {
            GetItemFromInventoryDicitionary(ui_id).UpdateCount(count);
        }
    }

    public void ClearItemElement(int ui_id)
    {
        if (IsItemInInventoryDictionary(ui_id) == true)
        {
            GetItemFromInventoryDicitionary(ui_id).ClearItem();
        }
    }

    public void ToggleEquipSelectedItem(int equippedUI_ID)
    {
        _inventoryUIItems[equippedUI_ID].ToggleEquippedIndicator();
    }
}
