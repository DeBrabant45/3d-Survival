using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentFootStepsSound : MonoBehaviour
{
    [SerializeField] AudioSource _footStepAudioSource;
    [SerializeField] AudioClip _footStepClip;
    [SerializeField] AudioClip _waterFootStepClip;
    [SerializeField] string _waterTag;
    private float _lastTime = 0;
    private float _duration;
    private AudioClip _currentStepClip;

    private void Start()
    {
        _duration = _footStepClip.length;
        _currentStepClip = _footStepClip;
    }

    public void PlayFootStepSound()
    {
        if(_lastTime == 0)
        {
            _footStepAudioSource.PlayOneShot(_currentStepClip);
        }
        if (Time.time - _lastTime >= _duration)
        {
            _lastTime = Time.time;
            _footStepAudioSource.PlayOneShot(_currentStepClip);
        }
    }    
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == _waterTag)
        {
            _currentStepClip = _waterFootStepClip;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == _waterTag)
        {
            _currentStepClip = _footStepClip;
        }
    }
}
