using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    [SerializeField] private AmmoItemSO _ammoItem;
    [SerializeField] private UIAmmo _uIAmmo;
    [SerializeField] private Transform _itemSlot;
    [SerializeField] private GunAmmo gunAmmo;
    private Action<AmmoItemSO, int> _onAmmoItemRequest;
    private Func<string, int, bool> _onAmmoAvailability;
    private Func<string, int> _onAmmoCountInStorage;
    private Func<ItemSO> _equippedItemRequest;

    public Action<AmmoItemSO, int> OnAmmoItemRequest { get => _onAmmoItemRequest; set => _onAmmoItemRequest = value; }
    public Func<string, int, bool> OnAmmoAvailability { get => _onAmmoAvailability; set => _onAmmoAvailability = value; }
    public Func<string, int> OnAmmoCountInStorage { get => _onAmmoCountInStorage; set => _onAmmoCountInStorage = value; }
    public Func<ItemSO> EquippedItemRequest { get => _equippedItemRequest; set => _equippedItemRequest = value; }

    private void Start()
    {
        RangedWeaponEvents.current.onAmmoAmountChange += SetEquippedAmmoAmountUI;
        RangedWeaponEvents.current.onRangedWeaponEquipped += SetWeaponIcon;
    }

    //private void Update()
    //{
    //    if (_uIAmmo.AmmoPanel.activeSelf == true)
    //    {
    //        SetEquippedAmmoAmount();
    //    }
    //}

    public void SetEquippedAmmoAmountUI()
    {
        var itemSlotGun = _itemSlot.GetComponentInChildren<GunAmmo>();
        if (itemSlotGun != null)
        {
            _uIAmmo.SetAmmoInGun(itemSlotGun.CurrentAmmoCount);
        }
    }

    public void SetAmmoCountInStorageUI()
    {
        var ammoInStorageAmount = _onAmmoCountInStorage.Invoke(_ammoItem.ID);
        _uIAmmo.SetStorageAmmoCount(ammoInStorageAmount);
    }

    public void SetWeaponIcon()
    {
        var weapon = _equippedItemRequest.Invoke();
        _uIAmmo.SetEquippedWeaponIcon(weapon.ImageSprite);
    }

    public bool IsAmmoAvailable()
    {
        bool enoughItemFlag = _onAmmoAvailability.Invoke(_ammoItem.ID, 1);
        return enoughItemFlag;
    }

    public void ReloadAmmoRequest(int amount)
    {
        _onAmmoItemRequest?.Invoke(_ammoItem, amount);
    }
}
