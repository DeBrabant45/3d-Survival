using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemPanel : ItemPanel, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Text _countText;
    [SerializeField] private int _itemCount;
    [SerializeField] bool _isHotBarItem = false;
    [SerializeField] private Image _equippedIndicator;
    private bool _isEmpty = true;
    private bool _equipped = false;

    public Action<int, bool> OnClickEvent;
    public Action<PointerEventData> DragStopCallBack;
    public Action<PointerEventData> DragContinueCallBack;
    public Action<PointerEventData, int> DragStartCallBack;
    public Action<PointerEventData, int> DropCallBack;
    public int ItemCount { get => _itemCount; }
    public bool IsEmpty { get => _isEmpty; }

    public override void SetItemUIElement(string name, Sprite image)
    {
        ItemName = name;
        _itemCount = 0;
        if (_isHotBarItem == false)
        {
            NameText.text = ItemName;
        }
        _countText.text = "";
        _isEmpty = false;
        SetImageSprite(image);
        if (_equipped)
        {
            ModityEquippendIndicatorAlpha(1);
        }
        else
        {
            ModityEquippendIndicatorAlpha(0);
        }
    }

    public void SetItemUIElement(string name, int count, Sprite image)
    {
        ItemName = name;
        _itemCount = count;
        if (_isHotBarItem == false)
        {
            NameText.text = ItemName;
        }
        _countText.text = (count < 0) ? "" : _itemCount + "";
        _isEmpty = false;
        SetImageSprite(image);
        if(_equipped)
        {
            ModityEquippendIndicatorAlpha(1);
        }
        else
        {
            ModityEquippendIndicatorAlpha(0);
        }
    }

    public void SwapWithData(string name, int count, Sprite image, bool isEmpty)
    {
        SetItemUIElement(name, count, image);
        this._isEmpty = isEmpty;
    }

    public override void ClearItem()
    {
        ItemName = "";
        _itemCount = -1;
        _countText.text = "";
        if (_isHotBarItem == false)
        {
            NameText.text = ItemName;
        }
        ResetImage();
        _isEmpty = true;
        ToggleHighLight(false);
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

    public void ToggleEquippedIndicator()
    {
        if(_equipped == false)
        {
            ModityEquippendIndicatorAlpha(1);
            _equipped = true;
        }
        else
        {
            ModityEquippendIndicatorAlpha(0);
            _equipped = false;
        }
    }

    private void ModityEquippendIndicatorAlpha(int alpha)
    {
        Color color = _equippedIndicator.color;
        color.a = Mathf.Clamp01(alpha);
        _equippedIndicator.color = color;
    }
}
