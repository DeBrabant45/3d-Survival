using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMeleeAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private WeaponItemSO _weaponItem;
    [SerializeField] private float _attackRate;
    private ItemSlot _itemSlot;

    public float AttackRate { get => _attackRate; }
    public WeaponItemSO EquippedWeapon { get => _weaponItem; }

    private void Start()
    {
        _itemSlot = GetComponent<ItemSlot>();
        _itemSlot.DamageCollider.OnCollisionSuccessful += PreformAttack;
    }

    public void PreformAttack(Collider hitObject)
    {
        var hittable = hitObject.GetComponent<IHittable>();
        var blockable = hitObject.GetComponent<IBlockable>();
        if (hittable != null && hitObject.gameObject != this.gameObject)
        {
            if (blockable != null)
            {
                blockable.Attacker = this.gameObject;
            }
            hittable.GetHit(_weaponItem);
        }
    }
}
