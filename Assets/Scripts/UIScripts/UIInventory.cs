﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryGeneralPanel;
    [SerializeField] private GameObject _hotbarPanel;
    [SerializeField] private GameObject _storagePanel;
    [SerializeField] private GameObject _storagePrefab;
    [SerializeField] private Canvas _canvas;
    private Dictionary<int, ItemPanelHelper> _inventoryUIItems = new Dictionary<int, ItemPanelHelper>();
    private Dictionary<int, ItemPanelHelper> _hotbarUIItems = new Dictionary<int, ItemPanelHelper>();
    private List<int> _hotbarItemElementsID = new List<int>();
    private RectTransform _draggableItem;
    private ItemPanelHelper _draggableItemPanel;

    public bool IsInventoryVisable { get => _inventoryGeneralPanel.activeSelf; }
    public int HotbarElementsCount { get => _hotbarUIItems.Count; }
    public RectTransform DraggableItem { get => _draggableItem; }
    public ItemPanelHelper DraggableItemPanel { get => _draggableItemPanel; }

    private void Awake()
    {
        _inventoryGeneralPanel.SetActive(false);
        foreach(Transform child in _hotbarPanel.transform)
        {
            ItemPanelHelper helper = child.GetComponent<ItemPanelHelper>();
            if(helper != null)
            {
                _hotbarUIItems.Add(helper.GetInstanceID(), helper);
                helper.SetUIHotBarItemToTrue();
            }
        }
        _hotbarItemElementsID = _hotbarUIItems.Keys.ToList();
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
            DestroyDraggedObject();
        }
    }

    public void PrepareInventoryItems(int playerStorageLimit)
    {
        DestoryAllItemsInInventory(playerStorageLimit);
        AddItemsToInventory(playerStorageLimit);
    }

    private void AddItemsToInventory(int playerStorageLimit)
    {
        for (int i = 0; i < playerStorageLimit; i++)
        {
            var element = Instantiate(_storagePrefab, Vector3.zero, Quaternion.identity, _storagePanel.transform);
            var itemHelper = element.GetComponent<ItemPanelHelper>();
            _inventoryUIItems.Add(itemHelper.GetInstanceID(), itemHelper);
        }
    }

    private void DestoryAllItemsInInventory(int playerStorageLimit)
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

    public List<ItemPanelHelper> GetUIElementsForInventory()
    {
        return _inventoryUIItems.Values.ToList();
    }

    public List<ItemPanelHelper> GetUIElementsForHotbar()
    {
        return _hotbarUIItems.Values.ToList();
    }

    public void DestroyDraggedObject()
    {
        if(_draggableItem != null)
        {
            Destroy(_draggableItem.gameObject);
            _draggableItemPanel = null;
            _draggableItem = null;
        }
    }

    public void CreateDraggableItem(int ui_id)
    {
        if(CheckItemInInventory(ui_id))
        {
            _draggableItemPanel = _inventoryUIItems[ui_id];
        }
        else
        {
            _draggableItemPanel = _hotbarUIItems[ui_id];
        }

        Image itemImage = _draggableItemPanel.ItemImage;
        var imageObject = Instantiate(itemImage, itemImage.transform.position, Quaternion.identity, _canvas.transform);
        imageObject.raycastTarget = false;
        imageObject.sprite = itemImage.sprite;

        _draggableItem = imageObject.GetComponent<RectTransform>();
        _draggableItem.sizeDelta = new Vector2(100, 100);

    }

    public bool CheckItemInInventory(int draggedItemID)
    {
        return _inventoryUIItems.ContainsKey(draggedItemID);
    }

    public void MoveDraggableItem(PointerEventData eventData)
    {
        var valueToAdd = eventData.delta / _canvas.scaleFactor;
        _draggableItem.anchoredPosition += valueToAdd;
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
        DestroyDraggedObject();
    }

    public void SwapUIHotbarItemToInventorySlot(int droppedItemID, int draggedItemID)
    {
        var tempName = _hotbarUIItems[draggedItemID].ItemName;
        var tempCount = _hotbarUIItems[draggedItemID].ItemCount;
        var tempSprite = _hotbarUIItems[draggedItemID].ItemImage.sprite;
        var tempIsEmpty = _hotbarUIItems[draggedItemID].IsEmpty;

        var droppedItemData = _inventoryUIItems[droppedItemID];

        _hotbarUIItems[draggedItemID].SwapWithData(droppedItemData.ItemName, droppedItemData.ItemCount, droppedItemData.ItemImage.sprite, droppedItemData.IsEmpty);
        _inventoryUIItems[droppedItemID].SwapWithData(tempName, tempCount, tempSprite, tempIsEmpty);
        DestroyDraggedObject();
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
        DestroyDraggedObject();
    }

    public void SwapUIInventoryItemToHotBarSlot(int droppedItemID, int draggedItemID)
    {
        var tempName = _inventoryUIItems[draggedItemID].ItemName;
        var tempCount = _inventoryUIItems[draggedItemID].ItemCount;
        var tempSprite = _inventoryUIItems[draggedItemID].ItemImage.sprite;
        var tempIsEmpty = _inventoryUIItems[draggedItemID].IsEmpty;
        var droppedItemData = _hotbarUIItems[droppedItemID];

        _inventoryUIItems[draggedItemID].SwapWithData(droppedItemData.ItemName, droppedItemData.ItemCount, droppedItemData.ItemImage.sprite, droppedItemData.IsEmpty);
        _hotbarUIItems[droppedItemID].SwapWithData(tempName, tempCount, tempSprite, tempIsEmpty);
        DestroyDraggedObject();
    }
}
