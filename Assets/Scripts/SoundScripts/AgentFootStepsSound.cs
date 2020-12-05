using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentFootStepsSound : MonoBehaviour
{
    [SerializeField] AudioSource _footStepAudioSource;
    [SerializeField] AudioClip _footStepClip;
    private float _lastTime = 0;
    private float _duration;

    private void Start()
    {
        _duration = _footStepClip.length;
    }

    public void PlayFootStepSound()
    {
        if(_lastTime == 0)
        {
            _footStepAudioSource.PlayOneShot(_footStepClip);
        }
        if (Time.time - _lastTime >= _duration)
        {
            _lastTime = Time.time;
            _footStepAudioSource.PlayOneShot(_footStepClip);
        }
    }
}
