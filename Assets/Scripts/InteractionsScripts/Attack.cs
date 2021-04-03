using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, IAttackable
{
    [SerializeField] private Transform _attackStartPosition;
    [SerializeField] private WeaponItemSO _weaponItem;
    [SerializeField] private float _attackDistance = 0.8f;
    [SerializeField] private float _attackRate;
    public Action OnAttackSuccessful { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float AttackRate { get => _attackRate; }

    public void DetectColliderInFront()
    {
        RaycastHit hit;
        if (Physics.SphereCast(_attackStartPosition.position, 0.2f, transform.forward, out hit, _attackDistance))
        {
            PreformAttack(hit.collider, hit.point);
        }
    }

    public void PreformAttack(Collider hitObject, Vector3 hitPosition)
    {
        var hittable = hitObject.GetComponent<IHittable>();
        var blockable = hitObject.GetComponent<IBlockable>();
        if (hittable != null)
        {
            if (blockable != null)
            {
                blockable.Attacker = this.gameObject;
            }
            hittable.GetHit(_weaponItem, hitPosition);
        }
    }
}
