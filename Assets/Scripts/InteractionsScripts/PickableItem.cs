using SVS.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable, IInventoryItem
{
    [SerializeField] private ItemSO _dataSource;
    [SerializeField] private int _count;

    public string ID => _dataSource.ID;

    public int Count
    {
        get
        {
            if(_dataSource.IsStackable == false)
            {
                return 1;
            }
            return _count;
        }
    }

    public bool IsStackable => _dataSource.IsStackable;

    public int StackLimit => _dataSource.StackLimit;

    public IInventoryItem PickUp()
    {
        InventoryEvents.Instance.ItemCollected(_dataSource, _count);
        return this;
    }

    public void SetCount(int value)
    {
        _count = value;
        if(_count == 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetDataSource(ItemSO data)
    {
        _dataSource = data;
    }
}
