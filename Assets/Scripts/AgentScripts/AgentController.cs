using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    [SerializeField] AgentMovement _movement;
    [SerializeField] PlayerInput _input;
    private BaseState currentState;

    public readonly BaseState movementState = new MovementState();
    public readonly BaseState jumpState = new JumpState();
    public PlayerInput Input { get => _input; }
    public AgentMovement Movement { get => _movement; }

    private void OnEnable()
    {
        _movement = GetComponent<AgentMovement>();
        _input = GetComponent<PlayerInput>();
        currentState = movementState;
        currentState.EnterState(this);
        AssignMovementInputListeners();
    }

    private void AssignMovementInputListeners()
    {
        _input.OnJump += HandleJump;
    }

    private void HandleJump()
    {
        currentState.HandleJumpInput();
    }

    private void Update()
    {
        currentState.Update();
    }

    private void OnDisable()
    {
        _input.OnJump -= currentState.HandleJumpInput;
    }

    public void TransitionToState(BaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
