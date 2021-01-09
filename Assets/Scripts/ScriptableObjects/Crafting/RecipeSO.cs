using SVS.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe Data", menuName = "CraftingData/RecipeSO")]
public class RecipeSO : ScriptableObject, IInventoryItem 
{
    [SerializeField] private string _recipeName;
    [SerializeField] private ItemSO _outCome;
    [SerializeField] [Range(1, 100)] private int _outComeQty = 1;
    [SerializeField] List<RecipeIngredients> _ingredientsRequired;

    public string ID => _outCome.ID;
    public int Count => _outComeQty;
    public bool IsStackable => _outCome.IsStackable;
    public int StackLimit => _outCome.StackLimit;
    public string RecipeName { get => _recipeName; }
    public List<RecipeIngredients> IngredientsRequired { get => _ingredientsRequired; set => _ingredientsRequired = value; }

    public Dictionary<string, int> GetIngredients()
    {
        Dictionary<string, int> ingredients = new Dictionary<string, int>();
        foreach(var ingredient in _ingredientsRequired)
        {
            ingredients.Add(ingredient.Ingredients.ID, ingredient.Count);
        }
        return ingredients;
    }

    public Sprite GetOutComeSprite()
    {
        return _outCome.ImageSprite;
    }

}
