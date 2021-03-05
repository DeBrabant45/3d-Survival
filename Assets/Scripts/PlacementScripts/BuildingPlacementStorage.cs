using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementStorage : MonoBehaviour
{
    private List<Structure> _playerStructures = new List<Structure>();

    public void SaveStructureReference(Structure structure)
    {
        _playerStructures.Add(structure);
    }
}
