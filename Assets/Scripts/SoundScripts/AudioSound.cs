using System;
using UnityEngine;

public class AudioSound : MonoBehaviour, IAudio
{
    [SerializeField] private AudioClip[] _soundClips;
    [SerializeField] private bool _isSoundRandom;
    private AudioSource _audioSource;
    private int _index = 0;

    public bool IsSoundRandom { get => _isSoundRandom; }

    private void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            throw new ArgumentException("No Audio source is present on " + this.gameObject.name);
        }
    }

    public void PlayRandomSound()
    {
        var randomIndex = UnityEngine.Random.Range(0, _soundClips.Length);
        _audioSource.PlayOneShot(_soundClips[randomIndex]);
    }

    public void PlaySoundInOrder()
    {
        if (_index >= _soundClips.Length)
        {
            _index = 0;
        }
        _audioSource.PlayOneShot(_soundClips[_index]);
        _index++;
    }
}
