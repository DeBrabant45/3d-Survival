using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSound : MonoBehaviour
{
    [SerializeField] private AudioClip _woodHitClip;
    [SerializeField] private AudioClip _stoneHitClip;
    public AudioClip WoodHitClip { get => _woodHitClip; }
    public AudioClip StoneHitClip { get => _stoneHitClip; }
}
