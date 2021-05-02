using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentTakeDamage : MonoBehaviour, IHittable
{
    private BlockAttack _blockAttack;
    private HurtEmissions _hurtEmissions;
    private AgentHealth _agentHealth;

    private void Start()
    {
        _blockAttack = GetComponent<BlockAttack>();
        _hurtEmissions = GetComponent<HurtEmissions>();
        _agentHealth = GetComponent<AgentHealth>();
    }

    public void GetHit(WeaponItemSO weapon)
    {
        if (_blockAttack != null && _blockAttack.IsBlockHitSuccessful() == true)
        {
            _blockAttack.OnBlockSuccessful.Invoke();
        }
        else
        {
            _agentHealth.ReduceHealth(weapon.GetDamageValue());
            _hurtEmissions.StartHurtCoroutine();
        }
    }
}
