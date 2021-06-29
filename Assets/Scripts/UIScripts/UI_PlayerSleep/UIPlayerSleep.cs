using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSleep : MonoBehaviour
{
    [SerializeField] GameObject _sleepPanel;
    [SerializeField] Button[] _buttons;
    [SerializeField] private Button _saveBtn;
    [SerializeField] private Button _exitBtn;
    [SerializeField] private Button _sleepBtn;
    [SerializeField] PlayerSleepManager _playerSleepManager;

    public void Start()
    {
        _saveBtn.onClick.AddListener(_playerSleepManager.SaveBed);
        _sleepBtn.onClick.AddListener(_playerSleepManager.PlayerSleep);
        _exitBtn.onClick.AddListener(_playerSleepManager.ExitUI);
        Hide();
    }

    public void Show()
    {
        _sleepPanel.SetActive(true);
    }

    public void Hide()
    {
        _sleepPanel.SetActive(false);
    }

    public void ToggleAllButtons()
    {
        foreach (var button in _buttons)
        {
            button.interactable = !button.interactable;
        }
    }
}
