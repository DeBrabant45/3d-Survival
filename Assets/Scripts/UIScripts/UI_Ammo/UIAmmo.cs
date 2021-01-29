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

    public GameObject AmmoPanel { get => _ammoPanel; }

    private void Start()
    {
        _ammoPanel.SetActive(false);
        RangedWeaponEvents.current.onRangedWeaponEquipped += ActivateAmmoPanel;
        RangedWeaponEvents.current.onRangedWeaponUnequipped += InActivateAmmoPanel;
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

    public void SetStorageAmmoCount(int ammoCount)
    {
        _ammoInStorageTxt.text = ammoCount + "";
    }

    public void SetEquippedWeaponIcon(Sprite weaponIcon)
    {
        _equippedWeaponIcon.sprite = weaponIcon;
    }
}
