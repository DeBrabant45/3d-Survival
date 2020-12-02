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
    private int inputVerticalDirection = 0;
    private bool isJumping = false;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        agentAnimations = GetComponent<HumanoidAnimations>();
    }

    public void HandleMovement(Vector2 input)
    {
        if(characterController.isGrounded)
        {
            if(input.y != 0)
            {
                if(input.y > 0)
                {
                    inputVerticalDirection = Mathf.CeilToInt(input.y);
                }
                else
                {
                    inputVerticalDirection = Mathf.FloorToInt(input.y);
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
        if(characterController.isGrounded)
        {
            if(moveDirection.magnitude > 0)
            {
                var animationSpeedMulitplier = agentAnimations.SetCorrectAnimation(desiredRotationAngle, _angleRotationThreshold, inputVerticalDirection);
                RotateAgent();
                moveDirection *= animationSpeedMulitplier;
            }
        }
        moveDirection.y -= _gravity;
        if(isJumping)
        {
            isJumping = false;
            moveDirection.y = _jumpSpeed;
            agentAnimations.SetMovementFloat(0);
        }
        characterController.Move(moveDirection * Time.deltaTime);
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
        if(characterController.isGrounded)
        {
            isJumping = true;
        }
    }
}
