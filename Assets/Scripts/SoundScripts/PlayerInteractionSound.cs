using System.Collections;
using UnityEngine;

namespace Assets.Scripts.SoundScripts
{
    public class PlayerInteractionSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _pickupItemClip;
        [SerializeField] private AudioClip _dropItemClip;

        public void PlayOneShotPickupItem()
        {
            _audioSource.PlayOneShot(_pickupItemClip);
        }        
        
        public void PlayOneShotDropItem()
        {
            _audioSource.PlayOneShot(_dropItemClip);
        }
    }
}