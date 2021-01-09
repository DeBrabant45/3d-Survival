using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesPanel : MonoBehaviour
{
    [SerializeField] GameObject _recipeItemsPanel;
    [SerializeField] GameObject _recipeItemPrefab;
    private Action<int> _onRecipeButtonClicked;
    private Dictionary<int, RecipeItemPanel> _recipeUIElements = new Dictionary<int, RecipeItemPanel>();

    public Action<int> OnRecipeButtonClicked { get => _onRecipeButtonClicked; set => _onRecipeButtonClicked = value; }

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

    private void OnRecipeClicked(int id)
    {
        if (_recipeUIElements.ContainsKey(id))
        {
            _onRecipeButtonClicked.Invoke(id);
        }
    }

    public List<int> PrepareRecipeItems(List<RecipeSO> Recipes)
    {
        ClearRecipeUI();
        _recipeUIElements.Clear();
        List<int> recipeUIIDs = new List<int>();
        foreach (var item in Recipes)
        {
            var element = Instantiate(_recipeItemPrefab, Vector3.zero, Quaternion.identity, _recipeItemsPanel.transform);
            var recipeHelper = element.GetComponent<RecipeItemPanel>();
            recipeHelper.OnClickEvent += OnRecipeClicked;
            _recipeUIElements.Add(element.GetInstanceID(), recipeHelper);
            recipeHelper.SetItemUIElement(item.RecipeName, item.GetOutComeSprite());
            recipeUIIDs.Add(element.GetInstanceID());
        }
        return recipeUIIDs;
    }
}
