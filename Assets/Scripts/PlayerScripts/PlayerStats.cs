using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour, IHittable
{
    [SerializeField] private UnityEvent _onDeath;

    private BlockAttack _blockAttack;
    private HurtEmissions _hurtEmissions;
    private AgentHealth _agentHealth;
    private AgentStamina _agentStamina;

    public BlockAttack BlockAttack { get => _blockAttack; }
    public AgentHealth AgentHealth { get => _agentHealth; }
    public AgentStamina AgentStamina { get => _agentStamina; }

    private void Awake()
    {
        _blockAttack = GetComponent<BlockAttack>();
        _hurtEmissions = GetComponent<HurtEmissions>();
        _agentHealth = GetComponent<AgentHealth>();
        _agentStamina = GetComponent<AgentStamina>();
        _agentHealth.OnHealthAmountEmpty += Death;
    }

    public void GetHit(WeaponItemSO weapon)
    {
        if (_blockAttack.IsBlockHitSuccessful() == true)
        {
            _blockAttack.OnBlockSuccessful.Invoke();
        }
        else
        {
            _agentHealth.ReduceHealth(weapon.GetDamageValue());
            _hurtEmissions.StartHurtCoroutine();
        }
    }

    private void Death()
    {
        _onDeath.Invoke();
    }
}
