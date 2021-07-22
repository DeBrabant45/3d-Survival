using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryEvents : MonoBehaviour
{
    private Action<ItemSO, int> _onItemCollected;
    private Action<string, int> _onItemInventory;
    public static InventoryEvents Instance;

    public Action<ItemSO, int> OnItemCollected { get => _onItemCollected; set => _onItemCollected = value; }
    public Action<string, int> OnItemInventory { get => _onItemInventory; set => _onItemInventory = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ItemCollected(ItemSO item, int count)
    {
        _onItemCollected?.Invoke(item, count);
    }

    public void CheckInventory(string item, int count)
    {
        _onItemInventory?.Invoke(item, count);
    }
}
