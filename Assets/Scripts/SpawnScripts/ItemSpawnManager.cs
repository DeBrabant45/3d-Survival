using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField] private static ItemSpawnManager _instance;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private string _pickableLayerMask;

    public static ItemSpawnManager Instance { get => _instance; }

    //Refactor this from a lazy loader
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
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
}
