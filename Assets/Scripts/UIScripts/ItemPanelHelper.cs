using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelHelper : MonoBehaviour
{
    [SerializeField] private Action<int, bool> _onClickEvent;
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _countText;
    [SerializeField] private Outline _outline;
    [SerializeField] private Sprite _backGroundSprite;
    [SerializeField] private string _itemName;
    [SerializeField] private string _itemCount;
    private bool _isEmpty = true;
    private bool _isHotBarItem = false;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        if(_itemImage.sprite == _backGroundSprite)
        {
            ClearItem();
        }
    }

    public void SetInventoryUIElement(string name, int count, Sprite image)
    {
        _itemName = name;
        _itemCount = count + "";
        if (_isHotBarItem == false)
            _nameText.text = _itemName;
        _countText.text = _itemCount;
        _isEmpty = false;
        SetImageSprite(image);
    }

    public void SwapWithData(string name, int count, Sprite image, bool isEmpty)
    {
        SetInventoryUIElement(name, count, image);
        this._isEmpty = isEmpty;
    }

    private void SetImageSprite(Sprite image)
    {
        _itemImage.sprite = image;
    }

    private void ClearItem()
    {
        _itemName = "";
        _itemCount = "";
        _countText.text = _itemCount;
        if(_isHotBarItem == false)
        {
            _nameText.text = _itemName;
        }
        ResetImage();
        _isEmpty = true;
        ToggleHighlight(false);
    }

    private void ToggleHighlight(bool value)
    {
        _outline.enabled = value;
    }

    private void ResetImage()
    {
        _itemImage.sprite = _backGroundSprite;
    }
}
