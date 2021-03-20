using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimations : MonoBehaviour
{
    private Animator _animator;
    private Action _onFinishedAttacking;
    private Action _onFinishedReloading;
    private Action _animationFunctionTrigger;

    public Action OnFinishedAttacking { get => _onFinishedAttacking; set => _onFinishedAttacking = value; }
    public Action OnFinishedReloading { get => _onFinishedReloading; set => _onFinishedReloading = value; }
    public Action OnAnimationFunctionTrigger { get => _animationFunctionTrigger; set => _animationFunctionTrigger = value; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetTriggerForAnimation(string name)
    {
        _animator.SetTrigger(name);
    }

    public void ResetTriggerForAnimation(string name)
    {
        _animator.ResetTrigger(name);
    }
    
    public void SetBoolForAnimation(string name, bool value)
    {
        _animator.SetBool(name, value);
    }

    public void FinishedAttackingCallBack()
    {
        _onFinishedAttacking.Invoke();
    }
    
    public void FinishedReloadingCallBack()
    {
        _onFinishedReloading.Invoke();
    }

    public void AnimationFunctionTriggerCallBack()
    {
        _animationFunctionTrigger.Invoke();
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
