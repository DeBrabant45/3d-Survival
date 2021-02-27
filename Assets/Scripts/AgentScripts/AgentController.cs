using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    [SerializeField] AgentMovement _movement;
    [SerializeField] AgentAimController _agentAimController;
    [SerializeField] PlayerInput _inputFromPlayer;
    [SerializeField] HumanoidAnimations _agentAnimations;
    [SerializeField] InventorySystem _inventorySystem;
    [SerializeField] DetectionSystem _detectionSystem;
    [SerializeField] GameManager _gameManager;
    [SerializeField] CraftingSystem _craftingSystem;
    [SerializeField] Transform _itemSlot;
    [SerializeField] Transform _backItemSlot;
    [SerializeField] AmmoSystem _ammoSystem;
    private BaseState _previousState;
    private BaseState _currentState;

    public readonly BaseState movementState = new MovementState();
    public readonly BaseState jumpState = new JumpState();
    public readonly BaseState fallingState = new FallingState();
    public readonly BaseState inventoryState = new InventoryState();
    public readonly BaseState interactState = new InteractState();
    public readonly BaseState menuState = new MenuState();
    public readonly BaseState meleeUnarmedAim = new MeleeUnarmedAimState();
    public readonly BaseState meleeUnarmedAttackOne = new MeleeUnarmedAttackOneState();
    public readonly BaseState meleeUnarmedAttackTwo = new MeleeUnarmedAttackTwoState();
    public readonly BaseState meleeUnarmedAttackThree = new MeleeUnarmedAttackThreeState();
    public readonly BaseState meleeUnarmedAttackFour = new MeleeUnarmedAttackFourState();
    public readonly BaseState meleeWeaponAttackOne = new MeleeWeaponAttackOneState();
    public readonly BaseState meleeWeaponAttackTwo = new MeleeWeaponAttackTwoState();
    public readonly BaseState meleeWeaponAttackThree = new MeleeWeaponAttackThreeState();
    public readonly BaseState meleeWeaponAimState = new MeleeWeaponAimState();
    public readonly BaseState rangedWeaponAimState = new RangedWeaponAimState();
    public readonly BaseState rangedWeaponAttackState = new RangedAttackState();
    public readonly BaseState reloadRangedWeaponState = new ReloadRangedWeaponState();
    public readonly BaseState equipItemState = new EquipItemState();
    public readonly BaseState unequipItemState = new UnequipItemState();

    public PlayerInput InputFromPlayer { get => _inputFromPlayer; }
    public AgentMovement Movement { get => _movement; }
    public HumanoidAnimations AgentAnimations { get => _agentAnimations; }
    public InventorySystem InventorySystem { get => _inventorySystem; }
    public DetectionSystem DetectionSystem { get => _detectionSystem; }
    public GameManager GameManager { get => _gameManager; }
    public BaseState PreviousState { get => _previousState; }
    public CraftingSystem CraftingSystem { get => _craftingSystem; }
    public Transform ItemSlot { get => _itemSlot; }
    public AmmoSystem AmmoSystem { get => _ammoSystem; }
    public AgentAimController AgentAimController { get => _agentAimController; }
    public Transform BackItemSlot { get => _backItemSlot; }

    private void OnEnable()
    {
        _movement = GetComponent<AgentMovement>();
        _inputFromPlayer = GetComponent<PlayerInput>();
        _agentAnimations = GetComponent<HumanoidAnimations>();
        _detectionSystem = GetComponent<DetectionSystem>();
        _agentAimController = GetComponent<AgentAimController>();
        _currentState = movementState;
        _currentState.EnterState(this);
        AssignInputListeners();
    }

    private void Start()
    {
        _craftingSystem.OnCheckResourceAvailability += _inventorySystem.CheckResourceAvailability;
        _craftingSystem.OnCheckInventoryIsFull += _inventorySystem.CheckInventoryIsFull;
        _craftingSystem.OnCraftItemRequest += _inventorySystem.CraftAnItem;
        _inventorySystem.OnInventoryStateChanged += _craftingSystem.RecheckIngredients;
        _ammoSystem.OnAmmoAvailability += _inventorySystem.CheckResourceAvailability;
        _ammoSystem.OnAmmoItemRequest += _inventorySystem.RemoveAmmoItemCount;
        _ammoSystem.OnAmmoCountInStorage += _inventorySystem.ItemAmountInStorage;
        _ammoSystem.EquippedItemRequest += _inventorySystem.EquippedItem;
    }

    private void AssignInputListeners()
    {
        _inputFromPlayer.OnJump += HandleJump;
        _inputFromPlayer.OnHotBarKey += HandleHotBarInput;
        _inputFromPlayer.OnToggleInventory += HandleInventoryInput;
        _inputFromPlayer.OnPrimaryAction += HandlePrimaryInput;
        _inputFromPlayer.OnSecondaryAction += HandleSecondaryInput;
        _inputFromPlayer.OnMenuToggledKey += HandleMenuInput;
        _inputFromPlayer.OnReload += HandleReloadInput;
        _inputFromPlayer.OnAim += HandleAimInput;
    }

    private void HandleAimInput()
    {
        _currentState.HandleEquipItemInput();
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

    private void HandleReloadInput()
    {
        _currentState.HandleReloadInput();
    }

    private void Update()
    {
        _currentState.Update();
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + _inputFromPlayer.MovementDirectionVector, _detectionSystem.DetectionRadius);
        }
    }

    private void OnDisable()
    {
        _inputFromPlayer.OnJump -= _currentState.HandleJumpInput;
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
