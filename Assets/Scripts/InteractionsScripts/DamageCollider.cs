using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private Collider _itemCollider;
    private Action<Collider> _onCollisionSuccessful;

    public Action<Collider> OnCollisionSuccessful { get => _onCollisionSuccessful; set => _onCollisionSuccessful = value; }
    public Collider ItemCollider { get => _itemCollider; }

    private void Start()
    {
        _itemCollider = GetComponent<Collider>();
        DisableCollider();
    }

    private void OnTriggerEnter(Collider collider)
    {
        _onCollisionSuccessful?.Invoke(collider);
    }

    public void EnableCollider()
    {
        _itemCollider.enabled = true;
    }

    public void DisableCollider()
    {
        _itemCollider.enabled = false;
    }
}
