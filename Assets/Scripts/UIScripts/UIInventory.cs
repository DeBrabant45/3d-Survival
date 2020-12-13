using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryGeneralPanel;
    [SerializeField] private GameObject _hotbarPanel;
    private Dictionary<int, ItemPanelHelper> _inventoryUIItems = new Dictionary<int, ItemPanelHelper>();
    private Dictionary<int, ItemPanelHelper> _hotbarUIItems = new Dictionary<int, ItemPanelHelper>();
    private List<int> _hotbarItemElementsID = new List<int>();

    public bool IsInventoryVisable { get => _inventoryGeneralPanel.activeSelf; }
    public int HotbarElementsCount { get => _hotbarUIItems.Count; }

    public List<ItemPanelHelper> GetUIElementsForHotbar()
    {
        return _hotbarUIItems.Values.ToList();
    }

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
}
