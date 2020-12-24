using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    [SerializeField] AgentMovement _movement;
    [SerializeField] PlayerInput _input;
    [SerializeField] HumanoidAnimations _agentAnimations;
    [SerializeField] InventorySystem _inventorySystem;
    [SerializeField] DetectionSystem _detectionSystem;
    private BaseState currentState;

    public readonly BaseState movementState = new MovementState();
    public readonly BaseState jumpState = new JumpState();
    public readonly BaseState fallingState = new FallingState();
    public readonly BaseState inventoryState = new InventoryState();
    public readonly BaseState interactState = new InteractState();
    public PlayerInput InputFromPlayer { get => _input; }
    public AgentMovement Movement { get => _movement; }
    public HumanoidAnimations AgentAnimations { get => _agentAnimations; }
    public InventorySystem InventorySystem { get => _inventorySystem; }
    public DetectionSystem DetectionSystem { get => _detectionSystem; }

    private void OnEnable()
    {
        _movement = GetComponent<AgentMovement>();
        _input = GetComponent<PlayerInput>();
        _agentAnimations = GetComponent<HumanoidAnimations>();
        currentState = movementState;
        currentState.EnterState(this);
        AssignInputListeners();
        _detectionSystem = GetComponent<DetectionSystem>();
    }

    private void AssignInputListeners()
    {
        _input.OnJump += HandleJump;
        _input.OnHotBarKey += HandleHotBarInput;
        _input.OnToggleInventory += HandleInventoryInput;
        _input.OnPrimaryAction += HandlePrimaryInput;
        _input.OnSecondaryAction += HandleSecondaryInput;
    }

    private void HandleSecondaryInput()
    {
        currentState.HandleSecondaryInput();
    }

    private void HandlePrimaryInput()
    {
        currentState.HandlePrimaryInput();
    }

    private void HandleJump()
    {
        currentState.HandleJumpInput();
    }

    private void HandleInventoryInput()
    {
        currentState.HandleInventoryInput();
    }

    private void HandleHotBarInput(int hotBarKey)
    {
        currentState.HandleHotBarInput(hotBarKey);
    }

    private void Update()
    {
        currentState.Update();
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + _input.MovementDirectionVector, _detectionSystem.DetectionRadius);
        }
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
