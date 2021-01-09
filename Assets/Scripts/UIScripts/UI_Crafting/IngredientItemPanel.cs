using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientItemPanel : ItemPanel
{
    [SerializeField] private Image _panelImage;
    [SerializeField] private Text _count;

    public void ModifyPanelAlpha(int value)
    {
        var panelColor = _panelImage.color;
        panelColor.a = Mathf.Clamp01(value);
        _panelImage.color = panelColor;
        Debug.Log(panelColor.a);
    }

    public void SetUnavailable()
    {
        ModifyPanelAlpha(1);
    }

    public void ResetAvailability()
    {
        ModifyPanelAlpha(0);
    }

    public override void SetItemUIElement(string name, Sprite image)
    {
        base.SetItemUIElement(name, image);
        _count.text = "x 0";
    }

    public void SetItemUIElement(string name, Sprite image, int count, bool isMax)
    {
        base.SetItemUIElement(name, image);
        _count.text = "x " + count;
        if(isMax)
        {
            ResetAvailability();
        }
        else
        {
            SetUnavailable();
        }
    }
}
