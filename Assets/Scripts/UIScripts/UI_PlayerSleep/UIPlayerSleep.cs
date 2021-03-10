using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSleep : MonoBehaviour
{
    [SerializeField] GameObject _sleepPanel;
    [SerializeField] Button[] _buttons;

    public void Start()
    {
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
