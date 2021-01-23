using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotWeaponParticleEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _itemParticleSystem;
    [SerializeField] private GameObject _itemSlot;

    private void Update()
    {
        AddParticleEffects();
    }

    private void AddParticleEffects()
    {
        if (_itemParticleSystem == null)
        {
            var particleEffect = _itemSlot.GetComponentInChildren<ParticleSystem>();
            if (particleEffect != null)
            {
                _itemParticleSystem = particleEffect;
            }
        }
    }

    public void PlayParticleEffect()
    {
        _itemParticleSystem.Play();
    }
}
