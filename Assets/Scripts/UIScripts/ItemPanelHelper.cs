using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanelHelper : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _countText;
    [SerializeField] private Outline _outline;
    [SerializeField] private Sprite _backGroundSprite;
    [SerializeField] private string _itemName;
    [SerializeField] private int _itemCount;
    [SerializeField] bool _isHotBarItem = false;
    private bool _isEmpty = true;

    public Action<int, bool> OnClickEvent;
    public Action<PointerEventData> DragStopCallBack;
    public Action<PointerEventData> DragContinueCallBack;
    public Action<PointerEventData, int> DragStartCallBack;
    public Action<PointerEventData, int> DropCallBack;
    public Image ItemImage { get => _itemImage; }
    public string ItemName { get => _itemName; }
    public int ItemCount { get => _itemCount; }
    public bool IsEmpty { get => _isEmpty; }

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
        _itemCount = count;
        if (_isHotBarItem == false)
        {
            _nameText.text = _itemName;
        }
        _countText.text = (count < 0) ? "" : _itemCount + "";
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

    public void ClearItem()
    {
        _itemName = "";
        _itemCount = -1;
        _countText.text = "";
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

    public bool SetUIHotBarItemToTrue()
    {
        return _isHotBarItem = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent.Invoke(GetInstanceID(), _isEmpty);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isEmpty)
            return;
        DragStartCallBack.Invoke(eventData, GetInstanceID());
    }

    public void UpdateCount(int count)
    {
        _itemCount = count;
        _countText.text = _itemCount + "";
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isEmpty)
            return;
        DragContinueCallBack.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isEmpty)
            return;
        DragStopCallBack.Invoke(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        DropCallBack.Invoke(eventData, GetInstanceID());
    }

    public void ToggleHighLight(bool value)
    {
        _outline.enabled = value;
    }
}
