using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVS.InventorySystem;

public interface IPickable
{
    IInventoryItem PickUp();

    void SetCount(int value);
}
