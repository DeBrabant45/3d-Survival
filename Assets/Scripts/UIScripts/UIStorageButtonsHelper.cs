using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStorageButtonsHelper : MonoBehaviour
{
    [SerializeField] private Button _useBtn;
    [SerializeField] private Button _dropBtn;
    private Action _onUseBtnClick;
    private Action _onDropBtnClick;

    private void Start()
    {
        _useBtn.onClick.AddListener(() => _onUseBtnClick?.Invoke());
        _dropBtn.onClick.AddListener(() => _onDropBtnClick?.Invoke());
    }

    public void HideAllButtons()
    {
        ToggleDropButton(false);
        ToggleUseButton(false);
    }

    public void ToggleUseButton(bool value)
    {
        _useBtn.interactable = value;
    }

    public void ToggleDropButton(bool value)
    {
        _dropBtn.interactable = value;
    }
}
