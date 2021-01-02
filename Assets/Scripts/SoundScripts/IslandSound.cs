using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSound : MonoBehaviour, IMapSounds
{
    [SerializeField] AudioSource _oceanAudioSource;

    public void StopAllMapSounds() 
    {
        _oceanAudioSource.Stop();
    }

    public void StartAllMapSounds()
    {
        _oceanAudioSource.Play();
    }

    public void PauseAllMapSounds()
    {
        _oceanAudioSource.Pause();
    }
}
