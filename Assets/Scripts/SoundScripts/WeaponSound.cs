using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    [SerializeField] private AudioSource _weaponSound;
    [SerializeField] private AudioClip _swingWoosh;

    public void PlayWeaponWooshSound()
    {
        _weaponSound.PlayOneShot(_swingWoosh);
    }
}
