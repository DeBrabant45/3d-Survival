using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentController : MonoBehaviour, ISaveable
{
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CraftingSystem _craftingSystem;
    [SerializeField] private Transform _itemSlotTransform;
    [SerializeField] private Transform _backItemSlotTransform;
    [SerializeField] private AmmoSystem _ammoSystem;
    [SerializeField] private BuildingPlacementStorage _buildingPlacementStorage;
    [SerializeField] private Vector3? _spawnPosition = null;
    [SerializeField] private WeaponItemSO _unarmedAttack;
    [SerializeField] private UnityEvent _onDeath;

    private WeaponItemSO _equippedItem;
    private AgentMovement _movement;
    private AgentAimController _agentAimController;
    private PlayerInput _inputFromPlayer;
    private HumanoidAnimations _agentAnimations;
    private DetectionSystem _detectionSystem;
    private BlockAttack _blockAttack;
    private ItemSlot _itemSlot;
    private AgentHealth _agentHealth;
    private AgentStamina _agentStamina;
    private BaseState _previousState;
    private BaseState _currentState;

    #region StateObjects 
    public readonly BaseState idleState = new IdleState();
    public readonly BaseState jumpState = new JumpState();
    public readonly BaseState fallingState = new FallingState();
    public readonly BaseState inventoryState = new InventoryState();
    public readonly BaseState interactState = new InteractState();
    public readonly BaseState menuState = new MenuState();
    // Attack States
    public readonly BaseState meleeUnarmedAttackState = new MeleeUnarmedAttackState();
    public readonly BaseState meleeWeaponAttackState = new MeleeWeaponAttackState();
    public readonly BaseState rangedWeaponAttackState = new RangedAttackState();
    // Attack Stances
    public readonly BaseState meleeWeaponAttackStanceState = new MeleeWeaponAttackStanceState();
    public readonly BaseState rangedWeaponAttackStanceState = new RangedWeaponAttackStanceState();
    //
    public readonly BaseState reloadRangedWeaponState = new ReloadRangedWeaponState();
    public readonly BaseState equipItemState = new EquipItemState();
    public readonly BaseState unequipItemState = new UnequipItemState();
    public readonly BaseState placementState = new PlacementState();
    public readonly BaseState blockStanceState = new BlockStanceState();
    public readonly BaseState blockReactionState = new BlockReactionState();
    public readonly BaseState hurtState = new HurtState();
    public readonly BaseState rangedWeaponAimState = new RangedWeaponAimState();
    #endregion

    public PlayerInput InputFromPlayer { get => _inputFromPlayer; }
    public AgentMovement Movement { get => _movement; }
    public HumanoidAnimations AgentAnimations { get => _agentAnimations; }
    public InventorySystem InventorySystem { get => _inventorySystem; }
    public DetectionSystem DetectionSystem { get => _detectionSystem; }
    public GameManager GameManager { get => _gameManager; }
    public BaseState PreviousState { get => _previousState; }
    public CraftingSystem CraftingSystem { get => _craftingSystem; }
    public Transform ItemSlotTransform { get => _itemSlotTransform; }
    public AmmoSystem AmmoSystem { get => _ammoSystem; }
    public AgentAimController AgentAimController { get => _agentAimController; }
    public Transform BackItemSlotTransform { get => _backItemSlotTransform; }
    public BuildingPlacementStorage BuildingPlacementStorage { get => _buildingPlacementStorage; }
    public WeaponItemSO UnarmedAttack { get => _unarmedAttack; }
    public ItemSlot ItemSlot { get => _itemSlot; }
    public WeaponItemSO EquippedItem { get => _equippedItem; }
    public BlockAttack BlockAttack { get => _blockAttack; }
    public AgentHealth AgentHealth { get => _agentHealth; }
    public AgentStamina AgentStamina { get => _agentStamina; }

    private void OnEnable()
    {
        _movement = GetComponent<AgentMovement>();
        _inputFromPlayer = GetComponent<PlayerInput>();
        _agentAnimations = GetComponent<HumanoidAnimations>();
        _detectionSystem = GetComponent<DetectionSystem>();
        _agentAimController = GetComponent<AgentAimController>();
        _itemSlot = GetComponent<ItemSlot>();
        _blockAttack = GetComponent<BlockAttack>();
        _agentHealth = GetComponent<AgentHealth>();
        _agentStamina = GetComponent<AgentStamina>();
        _currentState = idleState;
        _currentState.EnterState(this, EquippedItem);
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
        _inventorySystem.OnStructureUse += HandlePlacementInput;
        _inventorySystem.OnEquippedItemChange += HandleEquippedItem;
        _agentHealth.OnHealthAmountEmpty += Death;
        _equippedItem = _unarmedAttack;
    }

    private void AssignInputListeners()
    {
        _inputFromPlayer.OnJump += HandleJump;
        _inputFromPlayer.OnHotBarKey += HandleHotBarInput;
        _inputFromPlayer.OnToggleInventory += HandleInventoryInput;
        _inputFromPlayer.OnPrimaryAction += HandlePrimaryInput;
        _inputFromPlayer.OnSecondaryClickAction += HandleSecondaryClickInput;
        _inputFromPlayer.OnMenuToggledKey += HandleMenuInput;
        _inputFromPlayer.OnReload += HandleReloadInput;
        _inputFromPlayer.OnAim += HandleAimInput;
        _inputFromPlayer.OnSecondaryHeldDownAction += HandleSecondaryHeldDownInput;
        _inputFromPlayer.OnSecondaryUpAction += HandleSecondaryUpInput;
    }

    private void HandleEquippedItem()
    {
        if (_inventorySystem.WeaponEquipped == false)
        {
            _equippedItem = _unarmedAttack;
        }
        else
        {
            _equippedItem = (WeaponItemSO)ItemDataManager.Instance.GetItemData(_inventorySystem.EquippedWeaponID);
        }
    }

    private void HandleSecondaryUpInput()
    {
        _currentState.HandleSecondaryUpInput();
    }

    private void HandleSecondaryHeldDownInput()
    {
        _currentState.HandleSecondaryHeldDownInput();
    }

    private void HandleAimInput()
    {
        _currentState.HandleEquipItemInput();
    }

    private void HandleMenuInput()
    {
        _currentState.HandleMenuInput();
    }

    private void HandleSecondaryClickInput()
    {
        _currentState.HandleSecondaryClickInput();
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

    private void HandlePlacementInput()
    {
        _currentState.HandlePlacementInput();
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
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
        _currentState.EnterState(this, EquippedItem);
        //Debug.Log(_currentState + " new State");
    }

    public void SaveSpawnPoint()
    {
        _spawnPosition = transform.position;
    }

    private void RespawnPlayer()
    {
        if (_spawnPosition != null)
        {
            _movement.TeleportPlayerTo(_spawnPosition.Value + Vector3.up);
        }
    }

    public string GetJsonDataToSave()
    {
        var positionData = new TransformPosition
        {
            x = _spawnPosition.Value.x,
            y = _spawnPosition.Value.y,
            z = _spawnPosition.Value.z,
        };
        var playerData = new PlayerData
        {
            PlayerPosition = positionData,
            Health = _agentHealth.Health,
            Stamina = _agentStamina.Stamina,
        };

        return JsonConvert.SerializeObject(playerData);
    }

    public void LoadJsonData(string jsonData)
    {
        var playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
        _spawnPosition = new Vector3(playerData.PlayerPosition.x, playerData.PlayerPosition.y, playerData.PlayerPosition.z);
        RespawnPlayer();
        _agentHealth.Health = playerData.Health;
        _agentStamina.Stamina = playerData.Stamina;
    }

    private void Death()
    {
        _onDeath.Invoke();
    }
}