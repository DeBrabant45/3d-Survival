using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    protected CharacterController characterController;
    protected Vector3 moveDirection = Vector3.zero;
    protected float desiredRotationAngle = 0;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _angleRotationThreshold;
    private int inputVerticalDirection = 0;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
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
                RotateAgent();
            }
        }
        moveDirection.y -= _gravity;
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
}
