using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HotbarPanel : MonoBehaviour
{
    [SerializeField] private GameObject _hotbarPanel;
    private Dictionary<int, InventoryItemPanel> _hotbarUIItems = new Dictionary<int, InventoryItemPanel>();
    private List<int> _hotbarItemElementsID = new List<int>();

    public int HotbarElementsCount { get => _hotbarUIItems.Count; }
    public Dictionary<int, InventoryItemPanel> HotbarUIItems { get => _hotbarUIItems; set => _hotbarUIItems = value; }

    private void Awake()
    {
        LoadHotBarItemList();
    }

    private void LoadHotBarItemList()
    {
        foreach (Transform child in _hotbarPanel.transform)
        {
            InventoryItemPanel helper = child.GetComponent<InventoryItemPanel>();
            if (helper != null)
            {
                _hotbarUIItems.Add(helper.GetInstanceID(), helper);
                helper.SetUIHotBarItemToTrue();
            }
        }
        _hotbarItemElementsID = _hotbarUIItems.Keys.ToList();
    }

    public int GetHotBarElementUIIDWithIndex(int ui_index)
    {
        if (_hotbarItemElementsID.Count <= ui_index)
        {
            return -1;
        }
        return _hotbarItemElementsID[ui_index];
    }

    private InventoryItemPanel GetItemFromHotbarDicitionary(int ui_id)
    {
        Debug.Log("Item removed from the Hotbar");
        return _hotbarUIItems[ui_id];
    }

    public void ClearItemElement(int ui_id)
    {
        if(IsItemInHotbarDictionary(ui_id) == true)
        {
            GetItemFromHotbarDicitionary(ui_id).ClearItem();
        }
    }

    public void UpdateItemInfo(int ui_id, int count)
    {
        if (IsItemInHotbarDictionary(ui_id) == true)
        {
            GetItemFromHotbarDicitionary(ui_id).UpdateCount(count);
        }
    }

    public List<InventoryItemPanel> GetUIElementsForHotbar()
    {
        return _hotbarUIItems.Values.ToList();
    }

    public bool IsItemInHotbarDictionary(int ui_id)
    {
        return _hotbarUIItems.ContainsKey(ui_id);
    }

    public void SwapUIHotbarItemToHotBarSlot(int droppedItemID, int draggedItemID)
    {
        var tempName = _hotbarUIItems[draggedItemID].ItemName;
        var tempCount = _hotbarUIItems[draggedItemID].ItemCount;
        var tempSprite = _hotbarUIItems[draggedItemID].ItemImage.sprite;
        var tempIsEmpty = _hotbarUIItems[draggedItemID].IsEmpty;

        var droppedItemData = _hotbarUIItems[droppedItemID];
        //dragged Item
        _hotbarUIItems[draggedItemID].SwapWithData(droppedItemData.ItemName, droppedItemData.ItemCount, droppedItemData.ItemImage.sprite, droppedItemData.IsEmpty);
        //dropped item
        _hotbarUIItems[droppedItemID].SwapWithData(tempName, tempCount, tempSprite, tempIsEmpty);
    }

    public void SwapUIHotbarItemToInventorySlot(Dictionary<int, InventoryItemPanel> inventoryItems, int droppedItemID, int draggedItemID)
    {
        var tempName = _hotbarUIItems[draggedItemID].ItemName;
        var tempCount = _hotbarUIItems[draggedItemID].ItemCount;
        var tempSprite = _hotbarUIItems[draggedItemID].ItemImage.sprite;
        var tempIsEmpty = _hotbarUIItems[draggedItemID].IsEmpty;

        var droppedItemData = inventoryItems[droppedItemID];

        _hotbarUIItems[draggedItemID].SwapWithData(droppedItemData.ItemName, droppedItemData.ItemCount, droppedItemData.ItemImage.sprite, droppedItemData.IsEmpty);
        inventoryItems[droppedItemID].SwapWithData(tempName, tempCount, tempSprite, tempIsEmpty);
    }
}
