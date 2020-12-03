using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    protected CharacterController characterController;
    protected Vector3 moveDirection = Vector3.zero;
    protected float desiredRotationAngle = 0;
    protected HumanoidAnimations agentAnimations;

    [SerializeField] private float _gravity;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private int _angleRotationThreshold;
    private int _inputVerticalDirection = 0;
    private bool _isJumping = false;
    private bool _isJumpingCompleted = true;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        agentAnimations = GetComponent<HumanoidAnimations>();
    }

    public void HandleMovement(Vector2 input)
    {
        if(CharacterIsGrounded())
        {
            if(input.y != 0)
            {
                if(input.y > 0)
                {
                    _inputVerticalDirection = Mathf.CeilToInt(input.y);
                }
                else
                {
                    _inputVerticalDirection = Mathf.FloorToInt(input.y);
                }
                moveDirection = input.y * transform.forward * _movementSpeed;
            }
            else
            {
                agentAnimations.SetMovementFloat(0);
                moveDirection = Vector3.zero;
            }
        }
    }

    private void Update()
    {
        if (CharacterIsGrounded())
        {
            if (moveDirection.magnitude > 0)
            {
                var animationSpeedMulitplier = agentAnimations.SetCorrectAnimation(desiredRotationAngle, _angleRotationThreshold, _inputVerticalDirection);
                RotateAgent();
                moveDirection *= animationSpeedMulitplier;
            }
        }
        moveDirection.y -= _gravity;
        AgentIsJumping();
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void AgentIsJumping()
    {
        if (_isJumping == true)
        {
            _isJumping = false;
            _isJumpingCompleted = false;
            moveDirection.y = _jumpSpeed;
            agentAnimations.SetMovementFloat(0);
            agentAnimations.TriggerJumpAnimation();
        }
    }

    private void RotateAgent()
    {
        if(desiredRotationAngle > _angleRotationThreshold || desiredRotationAngle < -_angleRotationThreshold)
        {
            transform.Rotate(Vector3.up * desiredRotationAngle * _rotationSpeed * Time.deltaTime);
        }
    }

    public void HandleMovementDirection(Vector3 input)
    {
        desiredRotationAngle = Vector3.Angle(transform.forward, input);
        var crossProduct = Vector3.Cross(transform.forward, input).y;
        if(crossProduct < 0)
        {
            desiredRotationAngle *= -1;
        }
    }

    public void HandleJump()
    {
        if(CharacterIsGrounded())
        {
            _isJumping = true;
        }
    }

    public void StopMovementImmediatelly()
    {
        moveDirection = Vector3.zero;
    }

    public bool HasCompletedJumping()
    {
        return _isJumpingCompleted;
    }

    public void SetCompletedJumping()
    {
        _isJumpingCompleted = true;
    }

    public bool CharacterIsGrounded()
    {
        return characterController.isGrounded;
    }
}
