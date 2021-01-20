using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    [SerializeField] private AudioClip _weaponWoosh;
    public AudioClip WeaponWoosh { get => _weaponWoosh; set => _weaponWoosh = value; }

    public void PlayAttackSound(AudioSource audio)
    {
        audio.PlayOneShot(_weaponWoosh);
    }
}
