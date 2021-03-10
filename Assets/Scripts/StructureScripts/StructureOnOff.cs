using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureOnOff : Structure, IUsable
{
    [SerializeField]
    private bool _isUsable = true;
    [SerializeField]
    private GameObject[] _objectsToToggle;
    public bool IsUsable => _isUsable;

    public void Use()
    {
        foreach (var objectToToggle in _objectsToToggle)
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);
        }
    }
}
