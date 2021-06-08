using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNPC : MonoBehaviour, IEnemy
{
    [SerializeField] private int _emenyID;
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
    [SerializeField] ParticleSystem _defeatSmoke;
    [SerializeField] GameObject[] _eyes;
    [SerializeField] ItemSO _defeatedItemDrop;

    private NavMeshAgent _agent;
    private NPCMeleeAttack _meleeAttack;
    private Animator _animator;
    private HurtEmissions _hurtEmissions;
    private AgentColliders _agentColliders;
    private AgentHealth _agentHealth;
    private float _lastAttackTime = 0;
    private float _lastTimeMoved = 0f;

    public int EnemyID { get => _emenyID; }

    private void Awake()
    {
        _player = GameObject.Find("PlayerAgent").transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _meleeAttack = GetComponent<NPCMeleeAttack>();
        _hurtEmissions = GetComponent<HurtEmissions>();
        _agentColliders = GetComponent<AgentColliders>();
        _agentHealth = GetComponent<AgentHealth>();
        _agentHealth.OnHealthAmountEmpty += Death;
    }

    private void Update()
    {
        _isPlayerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _isPlayer);
        _isPlayerAttackRange = Physics.CheckSphere(transform.position, _attackRange, _isPlayer);

        if (_isPlayerInSightRange == false && _isPlayerAttackRange == false)
        {
            Patroling();
        }
        if (_isPlayerInSightRange && _isPlayerAttackRange == false)
        {
            ChasePlayer();
        }
        if (_isPlayerInSightRange && _isPlayerAttackRange)
        {
            AttackPlayer();
        }
        _animator.SetFloat("move", _agent.velocity.magnitude);
    }

    private void Patroling()
    {
        if (_isWalkPointSet == false)
        {
            SearchWalkPoint();
        }
        if (_isWalkPointSet)
        {
            _agent.SetDestination(_walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - _walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            if ((_lastTimeMoved + 5f) < Time.time)
            {
                _isWalkPointSet = false;
                _lastTimeMoved = Time.time;
            }
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(_walkPoint, -transform.up, 2f, _isGround))
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
        if ((_lastAttackTime + _meleeAttack.AttackRate) < Time.time)
        {
            _animator.SetTrigger(_meleeAttack.EquippedWeapon.AttackTriggerAnimation);
            _lastAttackTime = Time.time;
        }
        else
        {
            //transform.RotateAround(_player.transform.position, Vector3.up, 30 * Time.deltaTime);
        }
    }

    private void DisableDeadBody()
    {
        _hurtEmissions.DisablePlayerEmissions();
        _agentColliders.DisableColliders();
        DisableEyesParticals();
    }

    private void Death()
    {
        _animator.SetBool("IsDead", true);
        this.enabled = false;
        _animator.SetFloat("move", 0f);
        QuestEvents.Instance.EnemyDefeated(this);
    }

    private void DisableEyesParticals()
    {
        foreach(var eye in _eyes)
        {
            eye.SetActive(false);
        }
    }

    private void PlayDefeatSmoke()
    {
        _defeatSmoke.Play();
        ItemSpawnManager.Instance.CreateItemInPlace(this.transform.position, _defeatedItemDrop, 1);
    }

    private void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}
