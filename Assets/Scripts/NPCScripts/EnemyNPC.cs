using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNPC : MonoBehaviour, IHittable
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _isGround;
    [SerializeField] private LayerMask _isPlayer;
    [SerializeField] private Vector3 _walkPoint;
    [SerializeField] private bool _isWalkPointSet;
    [SerializeField] private float _walkPointRange;
    private float _lastTimeMoved = 0f;

    [SerializeField] float _sightRange;
    [SerializeField] float _attackRange;
    [SerializeField] bool _isPlayerInSightRange;
    [SerializeField] bool _isPlayerAttackRange;

    private NPCMeleeAttack _meleeAttack;
    private float _lastAttackTime = 0;

    //Life Pool
    [SerializeField] private int _health;
    //Die Hurt
    [SerializeField] private GameObject enemyModel;
    [SerializeField, Range(0, 1)] private float _hurtFeedbackTime = 0.2f;
    private MaterialHelper _materialHelper = new MaterialHelper();
    private Collider[] _colliders;
    private bool _isDead = false;

    private Animator _animator;

    public int Health => throw new NotImplementedException();

    private void Awake()
    {
        _player = GameObject.Find("PlayerAgent").transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _meleeAttack = GetComponent<NPCMeleeAttack>();
        _colliders = transform.GetComponentsInChildren<Collider>();
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
        if (_health <= 0 && _isDead == false)
        {
            _isDead = true;
            _animator.SetBool("IsDead", true);
            this.enabled = false;
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
    }

    private IEnumerator HurtCoroutine()
    {
        _materialHelper.EnableEmission(enemyModel, Color.red);
        yield return new WaitForSeconds(_hurtFeedbackTime);
        _materialHelper.DisableEmission(enemyModel);
    }

    public void Death()
    {
        StopAllCoroutines();
        _materialHelper.DisableEmission(enemyModel);
        foreach (var colider in _colliders)
        {
            colider.enabled = false;
        }
    }

    public void ReduceHealth(int amount)
    {
        _health -= amount;
    }

    public void GetHit(WeaponItemSO weapon)
    {
        ReduceHealth(weapon.GetDamageValue());
        StartCoroutine(HurtCoroutine());
    }
}
