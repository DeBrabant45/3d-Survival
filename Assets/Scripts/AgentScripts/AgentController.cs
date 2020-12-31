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
    [SerializeField] GameManager _gameManager;
    private BaseState _previousState;
    private BaseState _currentState;

    public readonly BaseState movementState = new MovementState();
    public readonly BaseState jumpState = new JumpState();
    public readonly BaseState fallingState = new FallingState();
    public readonly BaseState inventoryState = new InventoryState();
    public readonly BaseState interactState = new InteractState();
    public readonly BaseState menuState = new MenuState();
    public PlayerInput InputFromPlayer { get => _input; }
    public AgentMovement Movement { get => _movement; }
    public HumanoidAnimations AgentAnimations { get => _agentAnimations; }
    public InventorySystem InventorySystem { get => _inventorySystem; }
    public DetectionSystem DetectionSystem { get => _detectionSystem; }
    public GameManager GameManager { get => _gameManager; }
    public BaseState PreviousState { get => _previousState; }

    private void OnEnable()
    {
        _movement = GetComponent<AgentMovement>();
        _input = GetComponent<PlayerInput>();
        _agentAnimations = GetComponent<HumanoidAnimations>();
        _currentState = movementState;
        _currentState.EnterState(this);
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
        _input.OnMenuToggledKey += HandleMenuInput;
    }

    private void HandleMenuInput()
    {
        _currentState.HandleMenuInput();
    }

    private void HandleSecondaryInput()
    {
        _currentState.HandleSecondaryInput();
    }

    private void HandlePrimaryInput()
    {
        _currentState.HandlePrimaryInput();
    }

    private void HandleJump()
    {
        _currentState.HandleJumpInput();
    }

    private void HandleInventoryInput()
    {
        _currentState.HandleInventoryInput();
    }

    private void HandleHotBarInput(int hotBarKey)
    {
        _currentState.HandleHotBarInput(hotBarKey);
    }

    private void Update()
    {
        _currentState.Update();
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
        _input.OnJump -= _currentState.HandleJumpInput;
    }

    public void TransitionToState(BaseState state)
    {
        _previousState = _currentState;
        //Debug.Log(_previousState + " old State");
        _currentState = state;
        _currentState.EnterState(this);
        //Debug.Log(_currentState + " new State");
    }
}
