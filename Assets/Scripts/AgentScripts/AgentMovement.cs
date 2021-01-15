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
    private bool _temporaryMovementTriggered = false;
    private Quaternion _endRotationY;
    private float _temporaryDesiredRotationAngle;


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
                _temporaryMovementTriggered = false;
                if (input.y > 0)
                {
                    //while player is moving forward
                    _inputVerticalDirection = Mathf.CeilToInt(input.y);
                }
                else
                {
                    //while player is moving backward
                    _inputVerticalDirection = Mathf.FloorToInt(input.y);
                }
                moveDirection = input.y * transform.forward * _movementSpeed;
            }
            else
            {
                if(input.x != 0)
                {
                    if(_temporaryMovementTriggered == false)
                    {
                        _temporaryMovementTriggered = true;

                        int directionParameter = input.x > 0 ? 1 : -1;
                        if(directionParameter > 0)
                        {
                            _temporaryDesiredRotationAngle = 90;
                        }
                        else
                        {
                            _temporaryDesiredRotationAngle = -90;
                        }
                        //Calculate rotation based on right or left direction
                        _endRotationY = Quaternion.Euler(transform.localEulerAngles) * Quaternion.Euler(Vector3.up * _temporaryDesiredRotationAngle);
                    }
                    _inputVerticalDirection = 1;
                    moveDirection = transform.forward * _movementSpeed;
                }
                else
                {
                    _temporaryMovementTriggered = false;
                    agentAnimations.SetMovementFloat(0);
                    moveDirection = Vector3.zero;
                }
            }
        }
    }

    private void Update()
    {
        if (CharacterIsGrounded())
        {
            if (moveDirection.magnitude > 0 && _isJumpingCompleted)
            {
                //Sets the players animation based on players speed/input direction
                var animationSpeedMulitplier = agentAnimations.SetCorrectAnimation(desiredRotationAngle, _angleRotationThreshold, _inputVerticalDirection);
                if(_temporaryMovementTriggered == false)
                {
                    RotateAgent();
                }
                else
                {
                    RotateTemporary();
                }

                moveDirection *= animationSpeedMulitplier;
            }
        }
        moveDirection.y -= _gravity;
        AgentIsJumping();
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void RotateTemporary()
    {
        desiredRotationAngle = Quaternion.Angle(transform.rotation, _endRotationY);
        if(desiredRotationAngle > _angleRotationThreshold || desiredRotationAngle < -_angleRotationThreshold)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _endRotationY, Time.deltaTime * _rotationSpeed * 100);
        }
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
        //Rotates player based on users joystick direction
        if(desiredRotationAngle > _angleRotationThreshold || desiredRotationAngle < -_angleRotationThreshold)
        {
            transform.Rotate(Vector3.up * desiredRotationAngle * _rotationSpeed * Time.deltaTime);
        }
    }

    public void HandleMovementDirection(Vector3 input)
    {
        if(_temporaryMovementTriggered)
        {
            return;
        }
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

    public void StopMovement()
    {
        moveDirection = Vector3.zero;
        desiredRotationAngle = 0;
        agentAnimations.SetMovementFloat(0);
        _inputVerticalDirection = 0;
    }

    public bool HasCompletedJumping()
    {
        return _isJumpingCompleted;
    }

    public void SetCompletedJumping(bool value)
    {
        _isJumpingCompleted = value;
    }

    public void SetCompletedJumpTrue()
    {
        _isJumpingCompleted = true;
    }

    public void SetCompletedJumpFalse()
    {
        _isJumpingCompleted = false;
    }

    public bool CharacterIsGrounded()
    {
        return characterController.isGrounded;
    }
}
