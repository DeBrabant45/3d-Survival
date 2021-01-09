using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICrafting : MonoBehaviour
{
    [SerializeField] GameObject _craftingPanel;
    [SerializeField] GameObject _recipeItemsPanel;
    [SerializeField] GameObject _ingredientsMainPanel;
    [SerializeField] GameObject _ingredientElementsPanel;
    [SerializeField] GameObject _maxInventoryTxt;
    [SerializeField] GameObject _recipePrefab;
    [SerializeField] GameObject _ingredientPrefab;
    [SerializeField] Button _craftButton;
    private Dictionary<int, RecipeItemPanel> _recipeUIElements = new Dictionary<int, RecipeItemPanel>();
    private Action _onCraftButtonClicked;
    private Action<int> _onRecipeButtonClicked;

    public bool IsVisible { get => _craftingPanel.activeSelf; }
    public Action OnCraftButtonClicked { get => _onCraftButtonClicked; set => _onCraftButtonClicked = value; }
    public Action<int> OnRecipeButtonClicked { get => _onRecipeButtonClicked; set => _onRecipeButtonClicked = value; }

    private void Start()
    {
        ClearRecipeUI();
        _ingredientsMainPanel.SetActive(false);
        _craftingPanel.SetActive(false);
        _craftButton.onClick.AddListener(() => _onCraftButtonClicked.Invoke());
    }

    public void ShowIngredientsUI()
    {
        _ingredientsMainPanel.SetActive(true);
    }

    public void ClearIngredientsUI()
    {
        ClearUI(_ingredientElementsPanel.transform);
    }

    public void ClearRecipeUI()
    {
        ClearUI(_recipeItemsPanel.transform);
    }

    private void ClearUI(Transform panel)
    {
        foreach (Transform child in panel)
        {
            Destroy(child.gameObject);
        }
    }

    public void ToggleUI()
    {
        if(_craftingPanel.activeSelf == false)
        {
            _craftingPanel.SetActive(true);
        }
        else
        {
            _ingredientsMainPanel.SetActive(false);
            _craftingPanel.SetActive(false);
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

    private void OnRecipeClicked(int id)
    {
        if(_recipeUIElements.ContainsKey(id))
        {
            _onRecipeButtonClicked.Invoke(id);
        }
    }

    public void ShowMaxInventory()
    {
        var element = Instantiate(_maxInventoryTxt, Vector3.zero, Quaternion.identity, _ingredientElementsPanel.transform);
    }

    public void AddIngredient(string ingredientName, Sprite ingredientSprite, int ingredientCount, bool isMax)
    {
        var element = Instantiate(_ingredientPrefab, Vector3.zero, Quaternion.identity, _ingredientElementsPanel.transform);
        var ingredientHelper = element.GetComponent<IngredientItemPanel>();
        ingredientHelper.SetItemUIElement(ingredientName, ingredientSprite, ingredientCount, isMax);
    }

    public List<int> PrepareRecipeItems(List<RecipeSO> Recipes)
    {
        ClearRecipeUI();
        _recipeUIElements.Clear();
        List<int> recipeUIIDs = new List<int>();
        foreach (var item in Recipes)
        {
            var element = Instantiate(_recipePrefab, Vector3.zero, Quaternion.identity, _recipeItemsPanel.transform);
            var recipeHelper = element.GetComponent<RecipeItemPanel>();
            recipeHelper.OnClickEvent += OnRecipeClicked;
            _recipeUIElements.Add(element.GetInstanceID(), recipeHelper);
            recipeHelper.SetItemUIElement(item.RecipeName, item.GetOutComeSprite());
            recipeUIIDs.Add(element.GetInstanceID());
        }
        return recipeUIIDs;
    }
}
