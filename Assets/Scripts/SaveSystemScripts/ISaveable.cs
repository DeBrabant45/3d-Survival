using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    string GetJsonDataToSave();
    void LoadJsonData(string jsonData);
}
