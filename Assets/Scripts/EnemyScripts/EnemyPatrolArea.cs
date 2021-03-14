using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyPatrolArea : MonoBehaviour
{
    [SerializeField] private float _patrolRadius = 5f;
    [SerializeField] private SphereCollider _sphereCollider;
    private Action _onPlayerEnter;
    private Action _onPlayerExit;

    public Action OnPlayerEnter { get => _onPlayerEnter; set => _onPlayerEnter = value; }
    public Action OnPlayerExit { get => _onPlayerExit; set => _onPlayerExit = value; }

    // Start is called before the first frame update
    void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = _patrolRadius;
        _sphereCollider.isTrigger = true;
    }

    private void OnDrawGizmos()
    {
        if (_patrolRadius > 0)
        {
            Gizmos.DrawWireSphere(transform.position, _patrolRadius);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player has entered");
            _onPlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has exited");
            _onPlayerExit?.Invoke();
        }
    }
}
