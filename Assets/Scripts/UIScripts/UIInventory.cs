using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryGeneralPanel;
    [SerializeField] private GameObject _hotbarPanel;
    [SerializeField] private GameObject _storagePanel;
    [SerializeField] private GameObject _storagePrefab;
    private Dictionary<int, ItemPanelHelper> _inventoryUIItems = new Dictionary<int, ItemPanelHelper>();
    private Dictionary<int, ItemPanelHelper> _hotbarUIItems = new Dictionary<int, ItemPanelHelper>();
    private List<int> _hotbarItemElementsID = new List<int>();

    public bool IsInventoryVisable { get => _inventoryGeneralPanel.activeSelf; }
    public int HotbarElementsCount { get => _hotbarUIItems.Count; }

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
}
