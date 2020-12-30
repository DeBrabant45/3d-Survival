using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private IEnumerable<ISaveable> _itemsToSave;
    private string _filePath;

    private void Awake()
    {
        _filePath = Application.persistentDataPath + "/savedgame1.json";
    }

    private void Start()
    {
        _itemsToSave = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
    }

    public void SaveObjects()
    {
        Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
        foreach (var item in _itemsToSave)
        {
            var data = item.GetJsonDataToSave();
            var itemTypeName = item.GetType().ToString();
            dataDictionary.Add(itemTypeName, data);
        }
        var jsonString = JsonConvert.SerializeObject(dataDictionary);
        System.IO.File.WriteAllText(_filePath, jsonString);
        Debug.Log(_filePath);
    }

    public IEnumerator LoadSavedDataCoroutine(Action onFinishedLoading)
    {
        if(CheckSavedDataExists())
        {
            yield return new WaitForSecondsRealtime(2);
            var jsonSavedData = System.IO.File.ReadAllText(_filePath);
            Dictionary<string, string> dataDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonSavedData);
            foreach (var item in _itemsToSave)
            {
                var itemTypeName = item.GetType().ToString();
                if(dataDictionary.ContainsKey(itemTypeName))
                {
                    item.LoadJsonData(dataDictionary[itemTypeName]);
                }
                yield return new WaitForSecondsRealtime(0.1f);
            }
            onFinishedLoading?.Invoke();
        }
    }

    public bool CheckSavedDataExists()
    {
        return System.IO.File.Exists(_filePath);
    }
}
