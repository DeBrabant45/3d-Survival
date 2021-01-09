using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private List<RecipeSO> _craftingRecipes;
    [SerializeField] private CraftingPanel _craftingPanel;
    [SerializeField] private RecipesPanel _recipesPanel;
    [SerializeField] private IngredientsPanel _ingredientsPanel;
    private List<int> _recipeUIIds = new List<int>();
    private int _currentRecipeUIId = -1;
    private Action<RecipeSO> _onCraftItemRequest;
    private Func<string, int, bool> _onCheckResourceAvailability;
    private Func<bool> _onCheckInventoryIsFull;

    public Action<RecipeSO> OnCraftItemRequest { get => _onCraftItemRequest; set => _onCraftItemRequest = value; }
    public Func<string, int, bool> OnCheckResourceAvailability { get => _onCheckResourceAvailability; set => _onCheckResourceAvailability = value; }
    public Func<bool> OnCheckInventoryIsFull { get => _onCheckInventoryIsFull; set => _onCheckInventoryIsFull = value; }

    private void Start()
    {
        _recipesPanel.ClearRecipeUI();
        _recipesPanel.OnRecipeButtonClicked += RecipeClickedHandler;
        _craftingPanel.OnCraftButtonClicked += CraftRecipeHandler;
        _craftingPanel.BlockCraftButton();
    }

    public void ToggleCraftingUI(bool saveLastViewedRecipe = false)
    {
        _craftingPanel.ToggleUI();
        if(_craftingPanel.IsVisible == true)
        {
            _ingredientsPanel.HideIngredientsUI();
            _recipeUIIds = _recipesPanel.PrepareRecipeItems(_craftingRecipes);
        }
        if(saveLastViewedRecipe == false)
        {
            _currentRecipeUIId = -1;
        }
        if(_currentRecipeUIId != -1)
        {
            RecheckIngredients();
        }
    }

    public void RecheckIngredients()
    {
        if(_currentRecipeUIId != -1)
        {
            RecipeClickedHandler(_currentRecipeUIId);
        }
    }

    private void CraftRecipeHandler()
    {
        var recipeIndex = _recipeUIIds.IndexOf(_currentRecipeUIId);
        var recipe = _craftingRecipes[recipeIndex];
        _onCraftItemRequest.Invoke(recipe);
    }

    private void RecipeClickedHandler(int id)
    {
        _currentRecipeUIId = id;
        _ingredientsPanel.ClearIngredientsUI();
        var recipeIndex = _recipeUIIds.IndexOf(_currentRecipeUIId);
        var recipe = _craftingRecipes[recipeIndex];
        var ingredientsIdCount = recipe.GetIngredients();
        bool blockCraftBtn = false;
        foreach (var key in ingredientsIdCount.Keys)
        {
            bool enoughItemFlag = _onCheckResourceAvailability.Invoke(key, ingredientsIdCount[key]);
            if(blockCraftBtn == false)
            {
                blockCraftBtn = !enoughItemFlag;
            }
            _ingredientsPanel.AddIngredient(ItemDataManager.Instance.GetItemName(key), ItemDataManager.Instance.GetItemSprite(key), ingredientsIdCount[key], enoughItemFlag);
        }

        _ingredientsPanel.ShowIngredientsUI();
        if (blockCraftBtn)
        {
            _craftingPanel.BlockCraftButton();
        }
        else
        {
            _craftingPanel.UnblockCraftButton();
        }
        if(_onCheckInventoryIsFull.Invoke())
        {
            _ingredientsPanel.ShowMaxInventory();
        }
    }
}
