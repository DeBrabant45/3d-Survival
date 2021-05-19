using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryEvents : MonoBehaviour
{
    private Action<ItemSO> _onItemCollected;
    public static InventoryEvents Instance;

    public Action<ItemSO> OnItemCollected { get => _onItemCollected; set => _onItemCollected = value; }

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

    public void ItemCollected(ItemSO item)
    {
        _onItemCollected?.Invoke(item);
    }
}
