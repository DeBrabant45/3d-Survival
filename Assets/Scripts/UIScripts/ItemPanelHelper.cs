using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanelHelper : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _nameText;
    [SerializeField] private Outline _outline;
    [SerializeField] private Sprite _backGroundSprite;
    [SerializeField] private string _itemName;

    public Image ItemImage { get => _itemImage; protected set => _itemImage = value; }
    public string ItemName { get => _itemName; protected set => _itemName = value; }
    public Text NameText { get => _nameText; protected set => _nameText = value; }
    public Outline Outline { get => _outline; protected set => _outline = value; }
    public Sprite BackGroundSprite { get => _backGroundSprite; protected set => _backGroundSprite = value; }

    protected virtual void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        if(_itemImage.sprite == _backGroundSprite)
        {
            ClearItem();
        }
    }

    public virtual void SetItemUIElement(string name, Sprite image)
    {
        _itemName = name;
        _nameText.text = _itemName;
        SetImageSprite(image);
    }

    protected virtual void SetImageSprite(Sprite image)
    {
        _itemImage.sprite = image;
    }

    public virtual void ClearItem()
    {
        _itemName = "";
        _nameText.text = _itemName;
        ResetImage();
        ToggleHighLight(false);
    }

    protected virtual void ResetImage()
    {
        _itemImage.sprite = _backGroundSprite;
    }

    public virtual void ToggleHighLight(bool value)
    {
        _outline.enabled = value;
    }
}
