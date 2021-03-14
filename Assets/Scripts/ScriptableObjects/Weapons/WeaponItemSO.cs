using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generic Weapon Item", menuName = "InventoryData/WeaponItemSO")]
public class WeaponItemSO : ItemSO
{
    [SerializeField] int _minimalDamage = 0;
    [SerializeField] int _maximumDamage = 0;
    [SerializeField] [Range(0, 1)] float _criticalDamageChance = 0.2f;
    [SerializeField] WeaponType _weaponType;
    [SerializeField] float _weaponImpactForce;

    public WeaponType WeaponTypeSO { get => _weaponType; protected set => _weaponType = value; }
    public int MaximumDamage { get => _maximumDamage; }
    public float WeaponImpactForce { get => _weaponImpactForce; }

    public override bool IsUsable()
    {
        return true;
    }

    public int GetDamageValue()
    {
        int randomDamge = UnityEngine.Random.Range(_minimalDamage, _maximumDamage + 1);
        var randomCriticalChance = UnityEngine.Random.value;

        if (randomCriticalChance < _criticalDamageChance)
        {
            return _maximumDamage * 2;
        }
        return randomDamge;
    }
}

public enum WeaponType
{
    None,
    Ranged,
    Melee
}
