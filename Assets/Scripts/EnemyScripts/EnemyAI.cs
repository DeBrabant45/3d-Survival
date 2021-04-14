using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _isGround;
    [SerializeField] private LayerMask _isPlayer;
    [SerializeField] private Vector3 _walkPoint;
    [SerializeField] private bool _isWalkPointSet;
    [SerializeField] private float _walkPointRange;

    [SerializeField] float _sightRange;
    [SerializeField] float _attackRange;
    [SerializeField] bool _isPlayerInSightRange;
    [SerializeField] bool _isPlayerAttackRange;

    private void Awake()
    {
        _player = GameObject.Find("PlayerAgent").transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _isPlayerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _isPlayer);
        _isPlayerAttackRange = Physics.CheckSphere(transform.position, _attackRange, _isPlayer);

        if(_isPlayerInSightRange == false && _isPlayerAttackRange == false)
        {
            Patroling();
        }
        if(_isPlayerInSightRange && _isPlayerAttackRange == false)
        {
            ChasePlayer();
        }
        if(_isPlayerInSightRange && _isPlayerAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if(_isWalkPointSet == false)
        {
            SearchWalkPoint();
        }
        if(_isWalkPointSet)
        {
            _agent.SetDestination(_walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - _walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
        {
            _isWalkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(_walkPoint, -transform.up, 2f, _isGround))
        {
            _isWalkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }

    private void AttackPlayer()
    {
        _agent.SetDestination(transform.position);
        transform.LookAt(_player);
        Debug.Log("I'm attacking the player!");
    }

}
