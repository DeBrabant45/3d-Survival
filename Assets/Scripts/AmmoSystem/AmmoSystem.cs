using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    [SerializeField] private AmmoItemSO _ammoItem;
    private Action<AmmoItemSO, int> _onAmmoItemRequest;
    private Func<string, int, bool> _onAmmoAvailability;
    private Func<string, int> _onAmmoCountInStorage;
    private Func<ItemSO> _equippedItemRequest;

    public Action<AmmoItemSO, int> OnAmmoItemRequest { get => _onAmmoItemRequest; set => _onAmmoItemRequest = value; }
    public Func<string, int, bool> OnAmmoAvailability { get => _onAmmoAvailability; set => _onAmmoAvailability = value; }
    public Func<string, int> OnAmmoCountInStorage { get => _onAmmoCountInStorage; set => _onAmmoCountInStorage = value; }
    public Func<ItemSO> EquippedItemRequest { get => _equippedItemRequest; set => _equippedItemRequest = value; }
    public AmmoItemSO AmmoItem { get => _ammoItem; }

    public bool IsAmmoAvailable()
    {
        return _onAmmoAvailability.Invoke(_ammoItem.ID, 1); 
    }

    public void ReloadAmmoRequest(int amount)
    {
        _onAmmoItemRequest?.Invoke(_ammoItem, amount);
    }
}
