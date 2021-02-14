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

    public void TriggerMeleeAttackOneAnimation()
    {
        _animator.SetTrigger("meleeWeaponAttackOne");
    }    
    
    public void TriggerMeleeAttackTwoAnimation()
    {
        _animator.SetTrigger("meleeWeaponAttackTwo");
    }    
    
    public void TriggerMeleeAttackThreeAnimation()
    {
        _animator.SetTrigger("meleeWeaponAttackThree");
    }    
    
    public void TriggerUnarmedMeleeAttackOneAnimation()
    {
        _animator.SetTrigger("meleeUnarmedAttackOne");
    }    
    
    public void TriggerUnarmedMeleeAttackTwoAnimation()
    {
        _animator.SetTrigger("meleeUnarmedAttackTwo");
    }    
    
    public void TriggerUnarmedMeleeAttackThreeAnimation()
    {
        _animator.SetTrigger("meleeUnarmedAttackThree");
    }    
    
    public void TriggerUnarmedMeleeAttackFourAnimation()
    {
        _animator.SetTrigger("meleeUnarmedAttackFour");
    }

    public void TriggerShootAnimation()
    {
        _animator.SetTrigger("shoot");
    }

    public void TrigggerReloadWeaponAnimation()
    {
        _animator.SetTrigger("reloadWeapon");
    }  
    
    public void ActivateMeleeWeaponStanceAnimation()
    {
        _animator.SetBool("meleeWeaponStance", true);
    }

    public void DeactivateMeleeWeaponStanceAnimation()
    {
        _animator.SetBool("meleeWeaponStance", false);
    }    
    
    public void ActivateUnarmedStanceAnimation()
    {
        _animator.SetBool("meleeUnarmedStance", true);
    }

    public void DeactivateUnarmedStanceAnimation()
    {
        _animator.SetBool("meleeUnarmedStance", false);
    }

    public void FinishedAttackingCallBack()
    {
        _onFinishedAttacking.Invoke();
    }
    
    public void FinishedReloadingCallBack()
    {
        _onFinishedReloading.Invoke();
    }

    public float SetCorrectAnimation(float desiredRotationAngle, int angleThreshold, int inputVerticalDirection)
    {
        float currentAnimationSpeed = _animator.GetFloat("move");
        if(desiredRotationAngle > angleThreshold || desiredRotationAngle < -angleThreshold)
        {
            currentAnimationSpeed = BeginWalkingAnimation(inputVerticalDirection, currentAnimationSpeed);
        }
        else
        {
            currentAnimationSpeed = BeginRunningAnimation(inputVerticalDirection, currentAnimationSpeed);
        }
        return Mathf.Abs(currentAnimationSpeed);
    }

    private float BeginRunningAnimation(int inputVerticalDirection, float currentAnimationSpeed)
    {
        if (currentAnimationSpeed < 1)
        {
            currentAnimationSpeed += inputVerticalDirection * Time.deltaTime * 2;
            //Debug.Log("I'm running");
        }
        SetMovementFloat(Mathf.Clamp(currentAnimationSpeed, -1, 1));
        return currentAnimationSpeed;
    }

    private float BeginWalkingAnimation(int inputVerticalDirection, float currentAnimationSpeed)
    {
        if (Mathf.Abs(currentAnimationSpeed) < .2f)
        {
            currentAnimationSpeed += inputVerticalDirection * Time.deltaTime * 2;
            currentAnimationSpeed = Mathf.Clamp(currentAnimationSpeed, -0.2f, 0.2f);
            //Debug.Log("I'm walking");
        }
        SetMovementFloat(currentAnimationSpeed);
        return currentAnimationSpeed;
    }
}
