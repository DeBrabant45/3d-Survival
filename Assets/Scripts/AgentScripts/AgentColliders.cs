using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentColliders : MonoBehaviour
{
    private Collider[] _colliders;

    void Awake()
    {
        _colliders = transform.GetComponentsInChildren<Collider>();
    }

    public void DisableColliders()
    {
        foreach (var colider in _colliders)
        {
            colider.enabled = false;
        }
    }

    public void EnableColliders()
    {
        foreach (var colider in _colliders)
        {
            colider.enabled = true;
        }
    }
}
