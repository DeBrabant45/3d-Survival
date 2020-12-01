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
        moveDirection.y -= _gravity;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
