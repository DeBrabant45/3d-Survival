﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ResourceNode : MonoBehaviour, IHittable
{
    [SerializeField] private int _health = 3;
    [SerializeField] private MaterialItemSO _itemToSpawn;
    [SerializeField] private ResourceSound _resourceSound;
    [SerializeField] private AudioSource _audioSource;
    public int Health => _health;

    private void Start()
    {
        _audioSource.GetComponent<AudioSource>();
        SetAudio();
    }

    private void SetAudio()
    {
        switch (_itemToSpawn.ResourceType)
        {
            case ResourceType.None:
                throw new Exception("Resource can't be of type none");
            case ResourceType.Wood:
                _audioSource.clip = _resourceSound.WoodHitClip;
                break;
            case ResourceType.Stone:
                _audioSource.clip = _resourceSound.StoneHitClip;
                break;
            default:
                break;
        }
    }

    private IEnumerator DestroyObject(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void GetHit(WeaponItemSO weapon)
    {
        int resourceCountToSpawn = 1;
        if (weapon.GetType().Equals(typeof(ToolItemSO)))
        {
            resourceCountToSpawn = ((ToolItemSO)weapon).GetResourceHarvested(_itemToSpawn.ResourceType);
        }
        var randomSpawnRange = UnityEngine.Random.Range(0.5f, 0.7f);
        var spawnPosition = new Vector3(transform.position.x + randomSpawnRange, transform.position.y + randomSpawnRange, transform.position.z);
        ItemSpawnManager.Instance.CreateItemInPlace(spawnPosition, _itemToSpawn, resourceCountToSpawn);
        _audioSource.Play();
        _health--;
        if (_health <= 0)
        {
            StartCoroutine(DestroyObject(_audioSource.clip.length));
        }
    }
}
