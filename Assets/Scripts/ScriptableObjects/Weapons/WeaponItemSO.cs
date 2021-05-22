using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generic Weapon Item", menuName = "InventoryData/WeaponItemSO")]
public class WeaponItemSO : ItemSO
{
    [Header("Weapon Attack Settings")]
    [SerializeField] int _minimalDamage = 0;
    [SerializeField] int _maximumDamage = 0;
    [SerializeField] [Range(0, 1)] float _criticalDamageChance = 0.2f;
    [SerializeField] WeaponType _weaponType;
    [SerializeField] float _weaponImpactForce;

    [Header("Weapon Animation Settings")]
    [SerializeField] string _attackStanceAnimation;
    [SerializeField] string _attackTriggerAnimation;
    [SerializeField] string _blockStanceAnimation;
    [SerializeField] string _blockReactionAnimation;

    [Header("Weapon Equipped transform")]
    [SerializeField] Vector3 _equippedPosition;
    [SerializeField] Vector3 _equippedRotation;

    [Header("Weapon Unequipped transform")]
    [SerializeField] Vector3 _unequippedPosition;
    [SerializeField] Vector3 _unequippedRotation;

    [Header("Weapon Sounds")]
    [SerializeField] private AudioClip[] _soundClips;
    [SerializeField] private bool _isSoundRandom;

    public WeaponType WeaponTypeSO { get => _weaponType; protected set => _weaponType = value; }
    public int MaximumDamage { get => _maximumDamage; }
    public float WeaponImpactForce { get => _weaponImpactForce; }
    public string AttackStance { get => _attackStanceAnimation; }
    public string AttackTriggerAnimation { get => _attackTriggerAnimation; }
    public string BlockStanceAnimation { get => _blockStanceAnimation; }
    public string BlockReactionAnimation { get => _blockReactionAnimation; }
    public Vector3 EquippedPosition { get => _equippedPosition; }
    public Vector3 EquippedRotation { get => _equippedRotation; }
    public Vector3 UnequippedPosition { get => _unequippedPosition; }
    public Vector3 UnequippedRotation { get => _unequippedRotation; }
    public AudioClip[] SoundClips { get => _soundClips; }
    public bool IsSoundRandom { get => _isSoundRandom; }

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
