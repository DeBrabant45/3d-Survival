using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public WeaponItemSO EquippedWeapon { get; }
    void PreformAttack(Collider collider);
}
