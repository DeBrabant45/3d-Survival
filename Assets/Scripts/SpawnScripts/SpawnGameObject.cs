using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObject 
{
    private GameObject _gameObject;

    public SpawnGameObject(GameObject gameObject)
    {
        this._gameObject = gameObject;
    }

    public void CreateObject(Transform positionToSpawn)
    {
        GameObject.Instantiate(_gameObject, positionToSpawn.position, Quaternion.identity);
    }

    public void CreateTemporaryObject(Transform positionToSpawn)
    {
        var tempObject = GameObject.Instantiate(_gameObject, positionToSpawn.position, Quaternion.identity);
        GameObject.Destroy(tempObject, 2f);
    }    
    
    public void CreateTemporaryObject(Vector3 positionToSpawn, Quaternion quaternion, float timeUntilDestory)
    {
        var tempObject = GameObject.Instantiate(_gameObject, positionToSpawn, quaternion);
        GameObject.Destroy(tempObject, timeUntilDestory);
    }
}
