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

    public void OnAfterDeserialize()
    {
        throw new System.NotImplementedException();
    }

    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }
}

public enum ItemType
{
    None,
    Food,
    Weapon
}
