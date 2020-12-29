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

    public static ItemSpawnManager Instance { get => _instance; }

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

    public void CreateItemAtPlayersFeet(string itemID, int currentItemCount)
    {
        var itemPrfab = ItemDataManager.Instance.GetItemPrefab(itemID);
        var itemGameObject = Instantiate(itemPrfab, _playerTransform.position + Vector3.up, Quaternion.identity);
        PrepareItemGameObject(itemID, currentItemCount, itemGameObject);
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
                        CreateItemInPlace(spawnPosition, spawner.ItemToSpawn, spawner.Count);
                        randomPosition = GenerateRandomPosition(spawner.Radius);
                    }
                }
            }
            yield return new WaitForEndOfFrame();
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
}
