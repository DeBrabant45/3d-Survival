using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsPanel : MonoBehaviour
{
    [SerializeField] GameObject _ingredientsMainChildPanel;
    [SerializeField] GameObject _ingredientElementsPanel;
    [SerializeField] GameObject _maxInventoryTxtPrefab;
    [SerializeField] GameObject _ingredientPrefab;
    private RectTransform rectTransform;
    private RectTransform childTransform;

    private void Awake()
    {
        rectTransform = this.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(0, 0);
        childTransform = _ingredientsMainChildPanel.GetComponent<RectTransform>();
    }

    private void Start()
    {
        _ingredientsMainChildPanel.SetActive(false);
    }

    public void ShowIngredientsUI()
    {
        rectTransform.sizeDelta = childTransform.sizeDelta;
        _ingredientsMainChildPanel.SetActive(true);
    }

    public void ClearIngredientsUI()
    {
        ClearUI(_ingredientElementsPanel.transform);
    }

    private void ClearUI(Transform panel)
    {
        foreach (Transform child in panel)
        {
            Destroy(child.gameObject);
        }
    }

    public void HideIngredientsUI()
    {
        rectTransform.sizeDelta = new Vector2(0, 0);
        _ingredientsMainChildPanel.SetActive(false);
    }

    public void ShowMaxInventory()
    {
        var element = Instantiate(_maxInventoryTxtPrefab, Vector3.zero, Quaternion.identity, _ingredientElementsPanel.transform);
    }

    public void AddIngredient(string ingredientName, Sprite ingredientSprite, int ingredientCount, bool isMax)
    {
        var element = Instantiate(_ingredientPrefab, Vector3.zero, Quaternion.identity, _ingredientElementsPanel.transform);
        var ingredientHelper = element.GetComponent<IngredientItemPanel>();
        ingredientHelper.SetItemUIElement(ingredientName, ingredientSprite, ingredientCount, isMax);
    }
}
