using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotWeaponSound : MonoBehaviour
{
    [SerializeField] private AudioSource _weaponSound;
    [SerializeField] private GameObject _itemSlot;


    private void Update()
    {
        AddWeaponSound();    
    }

    private void AddWeaponSound()
    {
        if(_weaponSound == null)
        {
            var weaponSound = _itemSlot.GetComponentInChildren<AudioSource>();
            if (weaponSound != null)
            {
                _weaponSound = weaponSound;
            }
        }
    }

    public void PlayWeaponSound()
    {
        _weaponSound.PlayOneShot(_weaponSound.clip);
    }
}
