using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeItemPanel : ItemPanel, IPointerClickHandler
{
    private Action<int> _onClickEvent;

    public Action<int> OnClickEvent { get => _onClickEvent; set => _onClickEvent = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        int id = gameObject.GetInstanceID();
        _onClickEvent?.Invoke(id);
    }
}
