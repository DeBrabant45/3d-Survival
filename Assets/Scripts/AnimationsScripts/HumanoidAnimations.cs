using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimations : MonoBehaviour
{
    [SerializeField] Action _onFinishedAttacking;
    private Animator _animator;

    public Action OnFinishedAttacking { get => _onFinishedAttacking; set => _onFinishedAttacking = value; }

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

    public void TriggerAttackAnimation()
    {
        _animator.SetTrigger("attack");
    }

    public void FinishedAttackingCallBack()
    {
        _onFinishedAttacking.Invoke();
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
