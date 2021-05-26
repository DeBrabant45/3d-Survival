using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMeleeAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private WeaponItemSO _weaponItem;
    [SerializeField] private float _attackRate;
    private ItemSlot _itemSlot;
    private SpawnGameObject _spawnAttackHitEffect;

    public float AttackRate { get => _attackRate; }
    public WeaponItemSO EquippedWeapon { get => _weaponItem; }

    private void Start()
    {
        _itemSlot = GetComponent<ItemSlot>();
        _itemSlot.DamageCollider.OnCollisionSuccessful += PreformAttack;
        _spawnAttackHitEffect = new SpawnGameObject(_weaponItem.AttackHitEffect);
    } 

    public void PreformAttack(Collider hitObject)
    {
        var hittable = hitObject.GetComponent<IHittable>();
        var blockable = hitObject.GetComponent<IBlockable>();
        if (hittable != null && hitObject.tag != gameObject.tag)
        {
            if (blockable != null)
            {
                blockable.Attacker = this.gameObject;
            }
            _spawnAttackHitEffect.CreateTemporaryObject(_itemSlot.ItemSlotGameObject.transform);
            hittable.GetHit(_weaponItem);
        }
    }
}
