using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuresInteractionManager : MonoBehaviour
{
    [SerializeField] private PlayerSleepManager _playerSleepManager;

    public void ShowPlayerSleepUI()
    {
        _playerSleepManager.ShowUI();
    }
}
