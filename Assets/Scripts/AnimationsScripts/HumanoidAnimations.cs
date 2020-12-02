using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimations : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMovementFloat(float value)
    {
        _animator.SetFloat("move", value);
    }

    public float SetCorrectAnimation(float desiredRotationAngle, int angleThreshold, int inputVerticalDirection)
    {
        float currentAnimationSpeed = _animator.GetFloat("move");
        if(desiredRotationAngle > angleThreshold || desiredRotationAngle < -angleThreshold)
        {
            if(Mathf.Abs(currentAnimationSpeed) < .2f)
            {
                currentAnimationSpeed += inputVerticalDirection * Time.deltaTime * 2;
                currentAnimationSpeed = Mathf.Clamp(currentAnimationSpeed, -0.2f, 0.2f);
                //Debug.Log("I'm walking");
            }
            SetMovementFloat(currentAnimationSpeed);
        }
        else
        {
            if(currentAnimationSpeed < 1)
            {
                currentAnimationSpeed += inputVerticalDirection * Time.deltaTime * 2;
                //Debug.Log("I'm running");
            }
            SetMovementFloat(Mathf.Clamp(currentAnimationSpeed, -1, 1));
        }
        return Mathf.Abs(currentAnimationSpeed);
    }
}
