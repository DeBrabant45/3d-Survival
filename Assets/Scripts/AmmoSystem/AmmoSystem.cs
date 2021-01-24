using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    [SerializeField] private AmmoItemSO _ammoItem;
    private Action<AmmoItemSO> _onAmmoItemRequest;
    private Func<string, int, bool> _onAmmoAvailability;

    public Action<AmmoItemSO> OnAmmoItemRequest { get => _onAmmoItemRequest; set => _onAmmoItemRequest = value; }
    public Func<string, int, bool> OnAmmoAvailability { get => _onAmmoAvailability; set => _onAmmoAvailability = value; }

    private void Start()
    {

    }

    public bool IsAmmoAvailable()
    {
        bool enoughItemFlag = _onAmmoAvailability.Invoke(_ammoItem.ID, 1);
        return enoughItemFlag;
    }
}
