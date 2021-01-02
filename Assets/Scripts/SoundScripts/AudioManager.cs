using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject _mapSoundsGameObject;
    private IMapSounds _mapSounds;

    // Start is called before the first frame update
    void Start()
    {
        _mapSounds = _mapSoundsGameObject.GetComponent<IMapSounds>();
    }

    public void StopAllMapSounds()
    {
        _mapSounds.StopAllMapSounds();
    }

    public void PauseAllMapSounds()
    {
        _mapSounds.PauseAllMapSounds();
    }

    public void StartAllMapSounds()
    {
        _mapSounds.StartAllMapSounds();
    }
}
