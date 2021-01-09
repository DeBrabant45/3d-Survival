using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanel : MonoBehaviour
{
    [SerializeField] GameObject _craftingMainChildPanel;
    [SerializeField] Button _craftButton;
    private Action _onCraftButtonClicked;

    public bool IsVisible { get => _craftingMainChildPanel.activeSelf; }
    public Action OnCraftButtonClicked { get => _onCraftButtonClicked; set => _onCraftButtonClicked = value; }

    private void Start()
    {
        _craftingMainChildPanel.SetActive(false);
        _craftButton.onClick.AddListener(() => _onCraftButtonClicked.Invoke());
    }

    public void ToggleUI()
    {
        if(_craftingMainChildPanel.activeSelf == false)
        {
            _craftingMainChildPanel.SetActive(true);
        }
        else
        {
            _craftingMainChildPanel.SetActive(false);
        }
    }

    public void BlockCraftButton()
    {
        _craftButton.interactable = false;
    }

    public void UnblockCraftButton()
    {
        _craftButton.interactable = true;
    }
}
