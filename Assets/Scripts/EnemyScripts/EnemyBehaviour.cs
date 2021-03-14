using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour, IHittable
{
    [SerializeField] private EnemyPatrolArea _enemyPatrolArea;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _stoppingDistance = 2f;
    [SerializeField] private float _attackDamage;
    [SerializeField] private int _health;
    [SerializeField] private GameObject enemyModel;
    [SerializeField, Range(0, 1)] private float _hurtFeedbackTime = 0.2f;
    private bool _targetInRange = false;
    private bool _isDead;
    private Animator _animator;
    private MaterialHelper _materialHelper = new MaterialHelper();

    public int Health => _health;

    private void Start()
    {
        _enemyPatrolArea.OnPlayerEnter += ChasePlayer;
        _enemyPatrolArea.OnPlayerExit += StopChasingPlayer;
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.isStopped = true;
        _navMeshAgent.stoppingDistance = _stoppingDistance;
    }

    private void ChasePlayer()
    {
        _animator.SetBool("walk", true);
        _navMeshAgent.isStopped = false;
        _targetInRange = true;
    }

    private void StopChasingPlayer()
    {
        _animator.SetBool("walk", false);
        _navMeshAgent.isStopped = true;
        _targetInRange = false;
    }

    public void Attack()
    {
        _target.GetComponent<AgentController>().PlayerStat.ReduceHealth(_attackDamage);
    }

    public void Death()
    {
        StopAllCoroutines();
        _materialHelper.DisableEmission(enemyModel);
        this.enabled = false;
    }

    private void Update()
    {
        if(_navMeshAgent.isStopped == false)
        {
            _navMeshAgent.SetDestination(_target.transform.position);
            if (_targetInRange && _navMeshAgent.velocity.sqrMagnitude == 0)
            {
                transform.LookAt(_target.transform);
                _animator.SetBool("attack", true);
            }
            else
            {
                _animator.SetBool("attack", false);
            }
        }
    }

    public void GetHit(WeaponItemSO weapon, Vector3 hitpoint)
    {
        _health -= weapon.GetDamageValue();
        StartCoroutine(HurtCoroutine());
        if(_health <= 0 && _isDead == false)
        {
            _isDead = true;
            _animator.SetTrigger("death");
            _navMeshAgent.isStopped = true;
            _enemyPatrolArea.enabled = false;
        }
    }

    private IEnumerator HurtCoroutine()
    {
        _materialHelper.EnableEmission(enemyModel, Color.red);
        yield return new WaitForSeconds(_hurtFeedbackTime);
        _materialHelper.DisableEmission(enemyModel);
    }
}
