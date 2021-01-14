using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem 
{
    private RectTransform _draggableItem;
    private InventoryItemPanel _draggableItemPanel;

    public RectTransform DragItem { get => _draggableItem; }
    public InventoryItemPanel DraggableItemPanel { get => _draggableItemPanel; }

    public void DestroyDraggedObject()
    {
        if (_draggableItem != null)
        {
            GameObject.Destroy(_draggableItem.gameObject);
            _draggableItemPanel = null;
            _draggableItem = null;
        }
    }

    public void CreateDraggableItem(InventoryItemPanel uIitemsPanel, Canvas canvas)
    {
        _draggableItemPanel = uIitemsPanel;
        Image itemImage = _draggableItemPanel.ItemImage;
        var imageObject = GameObject.Instantiate(itemImage, itemImage.transform.position, Quaternion.identity, canvas.transform);
        imageObject.raycastTarget = false;
        imageObject.sprite = itemImage.sprite;

        _draggableItem = imageObject.GetComponent<RectTransform>();
        _draggableItem.sizeDelta = new Vector2(100, 100);

    }

    public void MoveDraggableItem(PointerEventData eventData, Canvas canvas)
    {
        var valueToAdd = eventData.delta / canvas.scaleFactor;
        _draggableItem.anchoredPosition += valueToAdd;
    }
}
