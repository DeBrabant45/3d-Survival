using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmo : MonoBehaviour
{
    [SerializeField] private GameObject _ammoPanel;
    [SerializeField] private Text _ammoInGunTxt;
    [SerializeField] private Text _ammoInStorageTxt;
    [SerializeField] private Image _equippedWeaponIcon;
    [SerializeField] private AmmoSystem _ammoSystem;

    private void Start()
    {
        _ammoPanel.SetActive(false);
        RangedWeaponEvents.current.onRangedWeaponEquipped += ActivateAmmoPanel;
        RangedWeaponEvents.current.onRangedWeaponUnequipped += InActivateAmmoPanel;
        RangedWeaponEvents.current.onRangedWeaponAmmoAmmountChange += SetAmmoInGun;
        RangedWeaponEvents.current.onRangedWeaponEquipped += SetEquippedWeaponIcon;
        RangedWeaponEvents.current.onInventoryHasChanged += SetStorageAmmoCount;
    }

    public void ActivateAmmoPanel()
    {
        _ammoPanel.SetActive(true);
    }

    public void InActivateAmmoPanel()
    {
        _ammoPanel.SetActive(false);
    }

    public void SetAmmoInGun(int ammoCount)
    {
        _ammoInGunTxt.text = ammoCount + "";
    }

    public void SetStorageAmmoCount()
    {
        _ammoInStorageTxt.text = _ammoSystem.OnAmmoCountInStorage.Invoke(_ammoSystem.AmmoItem.ID) + "";
    }

    public void SetEquippedWeaponIcon()
    {
        _equippedWeaponIcon.sprite = _ammoSystem.EquippedItemRequest.Invoke().ImageSprite;
    }
}
