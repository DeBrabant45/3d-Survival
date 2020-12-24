using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private string _iD;
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _imageSprite;
    [SerializeField] private GameObject _model;
    [SerializeField] private bool _isStackable;
    [SerializeField] [Range(1, 100)] private int _stackLimit = 100;
    [SerializeField] private ItemType _itemType;

    public string ID { get => _iD; }
    public string ItemName { get => _itemName; }
    public Sprite ImageSprite { get => _imageSprite; }
    public GameObject Model { get => _model; }
    public bool IsStackable { get => _isStackable; }
    public int StackLimit { get => _stackLimit; }

    public void OnAfterDeserialize() {}

    public void OnBeforeSerialize()
    {
        if(string.IsNullOrEmpty(this._iD))
        {
            this._iD = Guid.NewGuid().ToString("N");
        }
        if(string.IsNullOrEmpty(_itemName) && _model != null)
        {
            _itemName = _model.name;
        }
    }

    public Sprite GetImage()
    {
        return _imageSprite;
    }

    public ItemType GetItemType()
    {
        return _itemType;
    }

    public virtual bool IsUsable()
    {
        return false;
    }
}

public enum ItemType
{
    None,
    Food,
    Weapon
}
