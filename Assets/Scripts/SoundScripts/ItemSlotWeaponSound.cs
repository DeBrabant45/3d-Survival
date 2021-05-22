using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotWeaponSound : MonoBehaviour
{
    [SerializeField] private GameObject _itemSlot;
    private WeaponSound _audioSound;

    private void Start()
    {
        _audioSound = this.GetComponent<WeaponSound>();
    }

    private void Update()
    {
        if(_audioSound == null)
        {
            AddWeaponSound();
        }
    }

    private void AddWeaponSound()
    {
        var itemSlotChildSound = _itemSlot.GetComponentInChildren<WeaponSound>();
        if (itemSlotChildSound != null)
        {
            _audioSound = itemSlotChildSound;
        }
    }

    public void PlayWeaponSound()
    {
        if(_audioSound.IsSoundRandom)
        {
            _audioSound.PlayRandomSound();
        }
        else
        {
            _audioSound.PlaySoundInOrder();
        }

    }
}
