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

    private void Start()
    {
        //_ammoPanel.SetActive(false);
    }

    public void ToggleAmmoPanel()
    {
        if (_ammoPanel.activeSelf == false)
        {
            _ammoPanel.SetActive(true);
        }
        else
        {
            _ammoPanel.SetActive(false);
        }
    }

    public void SetAmmoInGun(int ammoCount)
    {
        _ammoInGunTxt.text = ammoCount + "";
    }

    public void SetStorageAmmoCount(int ammoCount)
    {
        _ammoInStorageTxt.text = ammoCount + "";
    }

    public void SetEquippedWeaponIcon(Image weaponIcon)
    {
        _equippedWeaponIcon = weaponIcon;
    }
}
