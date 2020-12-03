using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    [SerializeField] AgentMovement _movement;
    [SerializeField] PlayerInput _input;
    [SerializeField] HumanoidAnimations _agentAnimations;
    private BaseState currentState;

    public readonly BaseState movementState = new MovementState();
    public readonly BaseState jumpState = new JumpState();
    public readonly BaseState fallingState = new FallingState();
    public PlayerInput Input { get => _input; }
    public AgentMovement Movement { get => _movement; }
    public HumanoidAnimations AgentAnimations { get => _agentAnimations; }

    private void OnEnable()
    {
        _movement = GetComponent<AgentMovement>();
        _input = GetComponent<PlayerInput>();
        _agentAnimations = GetComponent<HumanoidAnimations>();
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
