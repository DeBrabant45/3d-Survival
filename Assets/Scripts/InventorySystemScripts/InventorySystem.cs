using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private UIInventory _uIInventory;

    private void Awake()
    {
        _uIInventory = GetComponent<UIInventory>();    
    }

    public void ToggleInventory()
    {
        if(_uIInventory.IsInventoryVisable == false)
        {
            //populate inventory
        }
        _uIInventory.ToggleUI();
    }
}
