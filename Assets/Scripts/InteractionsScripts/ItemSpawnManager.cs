using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField] private static ItemSpawnManager _instance;
    [SerializeField] private Transform _playerTransform;

    public static ItemSpawnManager Instance { get => _instance; }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }
}
