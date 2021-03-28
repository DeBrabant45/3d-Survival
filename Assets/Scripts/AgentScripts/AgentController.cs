using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour, ISaveable
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
    [SerializeField] BuildingPlacementStorage _buildingPlacementStorage;
    [SerializeField] Vector3? _spawnPosition = null;
    [SerializeField] PlayerStats _playerStat;
    [SerializeField] WeaponItemSO _unarmedAttack;
    private BaseState _previousState;
    private BaseState _currentState;

    #region StateObjects 
    public readonly BaseState movementState = new MovementState();
    public readonly BaseState jumpState = new JumpState();
    public readonly BaseState fallingState = new FallingState();
    public readonly BaseState inventoryState = new InventoryState();
    public readonly BaseState interactState = new InteractState();
    public readonly BaseState menuState = new MenuState();
    public readonly BaseState meleeUnarmedAim = new MeleeUnarmedAimState();
    public readonly BaseState meleeUnarmedAttackState = new MeleeUnarmedAttackState();
    public readonly BaseState meleeWeaponAttackState = new MeleeWeaponAttackState();
    public readonly BaseState meleeWeaponAimState = new MeleeWeaponAimState();
    public readonly BaseState rangedWeaponAimState = new RangedWeaponAimState();
    public readonly BaseState rangedWeaponAttackState = new RangedAttackState();
    public readonly BaseState reloadRangedWeaponState = new ReloadRangedWeaponState();
    public readonly BaseState equipItemState = new EquipItemState();
    public readonly BaseState unequipItemState = new UnequipItemState();
    public readonly BaseState placementState = new PlacementState();
    public readonly BaseState defenseState = new DefenseState();
    public readonly BaseState hurtState = new HurtState();
    #endregion

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
    public BuildingPlacementStorage BuildingPlacementStorage { get => _buildingPlacementStorage; }
    public PlayerStats PlayerStat { get => _playerStat; }
    public WeaponItemSO UnarmedAttack { get => _unarmedAttack; }

    private void OnEnable()
    {
        _movement = GetComponent<AgentMovement>();
        _inputFromPlayer = GetComponent<PlayerInput>();
        _agentAnimations = GetComponent<HumanoidAnimations>();
        _detectionSystem = GetComponent<DetectionSystem>();
        _agentAimController = GetComponent<AgentAimController>();
        _playerStat = GetComponent<PlayerStats>();
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
        _inventorySystem.OnStructureUse += HandlePlacementInput;
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
        _currentState.EnterState(this);
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
            Health = _playerStat.Health,
            Stamina = _playerStat.Stamina,
        };

        return JsonConvert.SerializeObject(playerData);
    }

    public void LoadJsonData(string jsonData)
    {
        var playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
        _spawnPosition = new Vector3(playerData.PlayerPosition.x, playerData.PlayerPosition.y, playerData.PlayerPosition.z);
        RespawnPlayer();
        _playerStat.Health = playerData.Health;
        _playerStat.Stamina = playerData.Stamina;
    }
}