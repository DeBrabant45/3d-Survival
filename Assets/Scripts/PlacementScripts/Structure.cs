using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public StructureItemSO Data { get; private set; }

    public void SetData(StructureItemSO structure)
    {
        Data = structure;
    }
}
