using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimations : MonoBehaviour
{
    private Animator _animator;
    private Action _onFinishedAttacking;
    private Action _onFinishedReloading;

    public Action OnFinishedAttacking { get => _onFinishedAttacking; set => _onFinishedAttacking = value; }
    public Action OnFinishedReloading { get => _onFinishedReloading; set => _onFinishedReloading = value; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TriggerLandingAnimation()
    {
        _animator.SetTrigger("land");
    }

    public void ResetTriggerLandingAnimation()
    {
        _animator.ResetTrigger("land");
    }

    public void TriggerJumpAnimation()
    {
        _animator.SetTrigger("jump");
    }

    public void TriggerFallAnimation()
    {
        _animator.SetTrigger("fall");
    }    
    
    public void ResetTriggerFallAnimation()
    {
        _animator.ResetTrigger("fall");
    }

    public void SetMovementFloat(float value)
    {
        _animator.SetFloat("move", value);
    }

    public void TriggerMeleeWeaponAnimation()
    {
        _animator.SetTrigger("meleeWeaponAttack");
    }      
    
    public void TriggerMeleeUnarmedAnimation()
    {
        _animator.SetTrigger("meleeUnarmedAttack");
    }    
      
    public void TriggerShootAnimation()
    {
        _animator.SetTrigger("shoot");
    }

    public void TrigggerReloadWeaponAnimation()
    {
        _animator.SetTrigger("reloadWeapon");
    }   

    public void IsMeleeWeaponStanceAnimationActive(bool value)
    {
        _animator.SetBool("meleeWeaponStance", value);
    }    
    
    public void IsMeleeUnarmedStanceAnimationActive(bool value)
    {
        _animator.SetBool("meleeUnarmedStance", value);
    }

    public void FinishedAttackingCallBack()
    {
        _onFinishedAttacking.Invoke();
    }
    
    public void FinishedReloadingCallBack()
    {
        _onFinishedReloading.Invoke();
    }

    public void SetAnimationInputX(float value)
    {
        _animator.SetFloat("InputX", value);
    }    
    
    public void SetAnimationInputY(float value)
    {
        _animator.SetFloat("InputY", value);
    }
}
