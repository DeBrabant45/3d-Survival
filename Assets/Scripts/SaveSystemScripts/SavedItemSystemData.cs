using SVS.InventorySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SavedItemSystemData
{
    public List<ItemData> PlayerStorageItems;
    public List<ItemData> HotbarStorageItems;
    public int PlayerStorageSize;
}
