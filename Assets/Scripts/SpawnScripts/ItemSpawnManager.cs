using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField] private static ItemSpawnManager _instance;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private string _pickableLayerMask;
    [SerializeField] private Transform _itemSpawnerParent;
    [SerializeField] private Material _transparentMaterial;
    private WeaponItemSO _equippedItem;
    private bool _isWeaponOnBackAndInHand = false;

    public static ItemSpawnManager Instance { get => _instance; }
    public Material TransparentMaterial { get => _transparentMaterial; }
    public bool IsWeaponOnBackAndInHand { get => _isWeaponOnBackAndInHand; }

    //Refactor this from a lazy loader
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
        SpawnAllItems();
    }

    private void Start()
    {
        StartCoroutine(SpawnAllItems());
    }

    public PlacementHelper CreateStructure(StructureItemSO structureData)
    {
        var structure = Instantiate(structureData.Model, _playerTransform.position + _playerTransform.forward, Quaternion.identity);
        var collider = structure.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        var rb = structure.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        var placementHelper = structure.AddComponent<PlacementHelper>();
        placementHelper.Initialize(_playerTransform);
        return placementHelper;
    }

    public void CreateItemAtPlayersFeet(string itemID, int currentItemCount)
    {
        var itemPrfab = ItemDataManager.Instance.GetItemPrefab(itemID);
        var itemGameObject = Instantiate(itemPrfab, _playerTransform.position + Vector3.up, Quaternion.identity);
        PrepareItemGameObject(itemID, currentItemCount, itemGameObject);
    }

    public void CreateItemInPlace(Vector3 hitpoint, MaterialItemSO itemToSpawn, int resourceCountToSpawn)
    {
        var itemGameObject = Instantiate(itemToSpawn.Model, hitpoint + Vector3.up * 0.2f, Quaternion.identity);
        PrepareItemGameObject(itemToSpawn.ID, resourceCountToSpawn, itemGameObject);
    }

    private void PrepareItemGameObject(string itemID, int currentItemCount, GameObject itemGameObject)
    {
        itemGameObject.AddComponent<BoxCollider>();
        var rb = itemGameObject.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        var pickableItem = itemGameObject.AddComponent<PickableItem>();
        pickableItem.SetCount(currentItemCount);
        pickableItem.SetDataSource(ItemDataManager.Instance.GetItemData(itemID));
        itemGameObject.layer = LayerMask.NameToLayer(_pickableLayerMask);
    }

    private IEnumerator SpawnAllItems()
    {
        foreach (Transform itemSpawner in _itemSpawnerParent)
        {
            var spawner = itemSpawner.GetComponent<ItemSpawner>();
            if (spawner != null)
            {
                SpawnItems(itemSpawner, spawner);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void SpawnItems(Transform itemSpawner, ItemSpawner spawner)
    {
        Vector3 randomPosition = GenerateRandomPosition(spawner.Radius);
        var spawnPosition = itemSpawner.position + randomPosition;
        if (spawner.SingleObject && spawner.ItemToSpawn.IsStackable)
        {
            CreateItemInPlace(spawnPosition, spawner.ItemToSpawn, spawner.Count);
        }
        else
        {
            for (int i = 0; i < spawner.Count; i++)
            {
                spawnPosition = itemSpawner.position + randomPosition;
                CreateItemInPlace(spawnPosition, spawner.ItemToSpawn, 1);
                randomPosition = GenerateRandomPosition(spawner.Radius);
            }
        }
    }

    public void RespawnItems()
    {
        foreach (Transform itemSpawner in _itemSpawnerParent)
        {
            var spawner = itemSpawner.GetComponent<ItemSpawner>();
            if (spawner != null && spawner.Respawnable)
            {
                SpawnItems(itemSpawner, spawner);
            }
        }
    }

    private void CreateItemInPlace(Vector3 spawnPosition, ItemSO itemToSpawn, int count)
    {
        var itemGameObject = Instantiate(itemToSpawn.Model, spawnPosition, Quaternion.identity, _itemSpawnerParent);
        PrepareItemGameObject(itemToSpawn.ID, count, itemGameObject);
    }

    private Vector3 GenerateRandomPosition(float radius)
    {
        var xRadius = Random.Range(-radius, radius);
        var yRadius = Random.Range(0, radius);
        var zRadius = Random.Range(-radius, radius);
        return new Vector3(xRadius, yRadius, zRadius);
    }

    public void RemoveItemFromPlayerHand()
    {
        foreach (Transform child in _playerTransform.GetComponent<AgentController>().ItemSlotTransform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SwapBackItemToPlayersHand()
    {
        var item = Instantiate(_equippedItem.Model, _playerTransform.GetComponent<AgentController>().ItemSlotTransform);
        item.transform.localPosition = _equippedItem.EquippedPosition;
        item.transform.localEulerAngles = _equippedItem.EquippedRotation;
        RemoveItemFromPlayersBack();
    }

    public void SwapHandItemToPlayersBack()
    {
        if(_isWeaponOnBackAndInHand == false)
        {
            var item = Instantiate(_equippedItem.Model, _playerTransform.GetComponent<AgentController>().BackItemSlotTransform);
            item.transform.localPosition = _equippedItem.UnequippedPosition;
            item.transform.localEulerAngles = _equippedItem.UnequippedRotation;
            RemoveItemFromPlayerHand();
        }
        else
        {
            RemoveItemFromPlayerHand();
            _isWeaponOnBackAndInHand = false;
        }
    }

    public void CreateItemObjectOnPlayersBack(string itemID)
    {
        if (_playerTransform.GetComponentInChildren<AgentController>().ItemSlotTransform.GetComponentInChildren<DamageCollider>() != null)
        {
            _isWeaponOnBackAndInHand = true;
        }
        else
        {
            _isWeaponOnBackAndInHand = false;
        }
        _equippedItem = (WeaponItemSO)ItemDataManager.Instance.GetItemData(itemID);
        var item = Instantiate(_equippedItem.Model, _playerTransform.GetComponent<AgentController>().BackItemSlotTransform);
        item.transform.localPosition = _equippedItem.UnequippedPosition;
        item.transform.localEulerAngles = _equippedItem.UnequippedRotation;
    }

    public void RemoveItemFromPlayersBack()
    {
        foreach (Transform child in _playerTransform.GetComponent<AgentController>().BackItemSlotTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
