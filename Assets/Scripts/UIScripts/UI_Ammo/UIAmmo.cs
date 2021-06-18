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
    private RangedWeaponItemSO _equippedRangedWeapon;

    private void Start()
    {
        _ammoPanel.SetActive(false);
        RangedWeaponEvents.Instance.OnRangedWeaponEquipped += ActivateAmmoPanel;
        RangedWeaponEvents.Instance.OnRangedWeaponUnequipped += InActivateAmmoPanel;
        RangedWeaponEvents.Instance.OnRangedWeaponAmmoAmmountChange += SetAmmoInGun;
        RangedWeaponEvents.Instance.OnInventoryHasChanged += SetStorageAmmoCount;
    }

    public void ActivateAmmoPanel(RangedWeaponItemSO equippedRangedItem)
    {
        _equippedRangedWeapon = equippedRangedItem;
        _ammoPanel.SetActive(true);
        SetStorageAmmoCount();
        SetEquippedWeaponIcon();
    }

    public void InActivateAmmoPanel()
    {
        if(_ammoPanel.activeSelf == true)
        {
            _ammoPanel.SetActive(false);
        }
    }

    public void SetAmmoInGun(int ammoCount)
    {
        _ammoInGunTxt.text = ammoCount + "";
    }

    public void SetStorageAmmoCount()
    {
        if(_equippedRangedWeapon != null)
        {
            _ammoInStorageTxt.text = _ammoSystem.OnAmmoCountInStorage.Invoke(_equippedRangedWeapon.AmmoType.ID) + "";
        }
    }

    public void SetEquippedWeaponIcon()
    {
        _equippedWeaponIcon.sprite = _equippedRangedWeapon.ImageSprite;
    }
}
