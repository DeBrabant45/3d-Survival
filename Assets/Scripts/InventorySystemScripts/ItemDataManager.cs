using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemDataManager : MonoBehaviour
{
    [SerializeField] private List<ItemSO> _ItemSOs = new List<ItemSO>();
    private Dictionary<string, ItemSO> _itemsDictionary = new Dictionary<string, ItemSO>();

    public static ItemDataManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(Instance);
        }
        foreach(var item in _ItemSOs)
        {
            _itemsDictionary.Add(item.ID, item);
        }
    }

    public string GetItemName(string id)
    {
        IDNotFoundException(id);
        return _itemsDictionary[id].ItemName;
    }

    public Sprite GetItemSprite(string id)
    {
        IDNotFoundException(id);
        return _itemsDictionary[id].ImageSprite;
    }

    public ItemSO GetItemData (string id)
    {
        IDNotFoundException(id);
        return _itemsDictionary[id];
    }

    public GameObject GetItemPrefab (string id)
    {
        IDNotFoundException(id);
        return _itemsDictionary[id].Model;
    }

    public bool IsItemUsabel(string id)
    {
        IDNotFoundException(id);
        return _itemsDictionary[id].IsUsable();
    }

    private void IDNotFoundException(string id)
    {
        if (_itemsDictionary.ContainsKey(id) == false)
        {
            throw new System.Exception("ItemDataManager doesn't have " + id);
        }
    }
}
