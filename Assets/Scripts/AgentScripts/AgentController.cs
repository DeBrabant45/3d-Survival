using AD.General;
using Assets.Scripts.SoundScripts;
using Newtonsoft.Json;
using System;
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
    private PlayerInteractionSound _interactionSound;

    #region Class Getters
    public PlayerInput InputFromPlayer { get => _inputFromPlayer; }
    public AgentMovement Movement { get => _movement; }
    public HumanoidAnimations AgentAnimations { get => _agentAnimations; }
    public InventorySystem InventorySystem { get => _inventorySystem; }
    public DetectionSystem DetectionSystem { get => _detectionSystem; }
    public GameManager GameManager { get => _gameManager; }
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
    public PlayerInteractionSound InteractionSound { get => _interactionSound; }
    #endregion

    private void Awake()
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
        _interactionSound = GetComponent<PlayerInteractionSound>();
        _equippedItem = _unarmedAttack;
    }

    private void OnEnable()
    {
        _craftingSystem.OnCheckResourceAvailability += _inventorySystem.CheckResourceAvailability;
        _craftingSystem.OnCheckInventoryIsFull += _inventorySystem.CheckInventoryIsFull;
        _craftingSystem.OnCraftItemRequest += _inventorySystem.CraftAnItem;
        _inventorySystem.OnInventoryStateChanged += _craftingSystem.RecheckIngredients;
        _ammoSystem.OnAmmoAvailability += _inventorySystem.CheckResourceAvailability;
        _ammoSystem.OnAmmoItemRequest += _inventorySystem.RemoveAmmoItemCount;
        _ammoSystem.OnAmmoCountInStorage += _inventorySystem.ItemAmountInStorage;
        _ammoSystem.EquippedItemRequest += _inventorySystem.EquippedItem;
        _inventorySystem.OnEquippedItemChange += HandleEquippedItem;
        _agentHealth.OnHealthAmountEmpty += Death;
        _inventorySystem.OnItemHasBeenDropped += _interactionSound.PlayOneShotDropItem;
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

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + _inputFromPlayer.MovementDirectionVector, _detectionSystem.DetectionRadius);
        }
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