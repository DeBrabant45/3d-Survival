using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementStorage : MonoBehaviour, ISaveable
{
    private List<Structure> _playerStructures = new List<Structure>();

    public string GetJsonDataToSave()
    {
        List<SavedStructureData> savedStructures = new List<SavedStructureData>();
        foreach (var structure in _playerStructures)
        {
            var euler = structure.transform.rotation.eulerAngles;
            savedStructures.Add(new SavedStructureData
            {
                ID = structure.Data.ID,
                PositionX = structure.transform.position.x,
                PositionY = structure.transform.position.y,
                PositionZ = structure.transform.position.z,
                RotationX = euler.x,
                RotationY = euler.y,
                RotationZ = euler.z,
            });
        }
        string data = JsonConvert.SerializeObject(savedStructures);
        return data;
    }

    public void LoadJsonData(string jsonData)
    {
        List<SavedStructureData> savedStructures = JsonConvert.DeserializeObject<List<SavedStructureData>>(jsonData);
        foreach (var data in savedStructures)
        {
            var itemData = ItemDataManager.Instance.GetItemData(data.ID);
            var structureToPlace = ItemSpawnManager.Instance.CreateStructure((StructureItemSO)itemData);
            structureToPlace.PrepareForMovement();
            var structureReference = structureToPlace.PrepareForPlacement();
            Vector3 position = new Vector3(data.PositionX, data.PositionY, data.PositionZ);
            Quaternion rotation = Quaternion.Euler(data.RotationX, data.RotationX, data.RotationZ);
            structureReference.transform.position = position;
            structureReference.transform.rotation = rotation;
            structureReference.SetData((StructureItemSO)itemData);
            SaveStructureReference(structureReference);
        }
    }

    public void SaveStructureReference(Structure structure)
    {
        _playerStructures.Add(structure);
    }
}
