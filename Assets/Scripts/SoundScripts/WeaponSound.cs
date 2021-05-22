using System;
using UnityEngine;

public class WeaponSound : MonoBehaviour, IAudio
{
    [SerializeField] private WeaponItemSO _weapon;
    private AudioSource _audioSource;
    private int _index = 0;

    public bool IsSoundRandom { get => _weapon.IsSoundRandom; }

    private void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            throw new ArgumentException("No Audio source is present on " + this.gameObject.name);
        }
        if(_weapon == null)
        {
            throw new ArgumentException("No Weapon source is present on " + this.gameObject.name);
        }
    }

    public void PlayRandomSound()
    {
        var randomIndex = UnityEngine.Random.Range(0, _weapon.SoundClips.Length);
        _audioSource.PlayOneShot(_weapon.SoundClips[randomIndex]);
    }

    public void PlaySoundInOrder()
    {
        if (_index >= _weapon.SoundClips.Length)
        {
            _index = 0;
        }
        _audioSource.PlayOneShot(_weapon.SoundClips[_index]);
        _index++;
    }
}
