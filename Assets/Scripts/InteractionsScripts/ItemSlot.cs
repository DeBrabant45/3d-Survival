using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] GameObject _itemSlot;
    [SerializeField] DamageCollider _damageCollider;

    public DamageCollider DamageCollider { get => _damageCollider; set => _damageCollider = value; }

    public void Update()
    {
        if (_damageCollider == null)
        {
            var childCollider = _itemSlot.GetComponentInChildren<DamageCollider>();
            if (childCollider != null)
            {
                _damageCollider = childCollider;
            }

        }
    }

    public void EnableWeaponCollider()
    {
        if(_damageCollider.ItemCollider != null)
        {
            _damageCollider.EnableCollider();
        }
    }

    public void DisableWeaponCollider()
    {
        if (_damageCollider.ItemCollider != null)
        {
            _damageCollider.DisableCollider();
        }
    }    
}
