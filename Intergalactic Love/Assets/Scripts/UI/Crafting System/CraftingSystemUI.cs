using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystemUI : MonoBehaviour
{
    public RecipeList recipeList;
    public CraftingZone craftingZone;

    public List<ItemData> ingredientInCrafting;

    private PlayerInventory playerInventory;

    private Recipe presetRecipe;

    public void Initialize()
    {
        playerInventory = GameManager.gm.player.playerInventory;
        craftingZone.Initialize(this);
    }

    public void OnOpenCraftingTab()
    {
        recipeList.Initialize(this);
        ingredientInCrafting = new List<ItemData>();
    }

    public void OnClickRecipe(Recipe recipe)
    {
        if (CanCraftItem(recipe))
        {
            craftingZone.SetRecipe(recipe);
        }
    }

    public void PresetRecipe(Recipe recipe)
    {
        presetRecipe = recipe;
    }

    public void Craft()
    {
        if (presetRecipe != null)
        {
            presetRecipe = FindAssociatedRecipe();
            if (presetRecipe == null)
            {
                Debug.Log("NO RECIPE");
                return;
            }
        }

        foreach (ItemData ingredient in presetRecipe.ingredients)
        {
            playerInventory.RemoveItemFromInventory(ingredient, 1);
        }

        playerInventory.AddItemToInventory(presetRecipe.result, presetRecipe.amount);
    }

    public bool CanCraftItem(Recipe recipe)
    {
        Dictionary<ItemData, int> ingredients = new Dictionary<ItemData, int>();

        foreach (ItemData ing in recipe.ingredients)
        {
            if (ingredients.ContainsKey(ing))
                ingredients[ing]++;
            else
                ingredients[ing] = 1;
        }

        foreach (KeyValuePair<ItemData, int> val in ingredients)
        {
            if (!playerInventory.inventory.ContainsKey(val.Key))
                return false;

            if (playerInventory.inventory[val.Key] < val.Value)
            {
                return false;
            }
        }
        return true;
    }

    public Recipe FindAssociatedRecipe()
    {
        return GameManager.gm.recipeManager.FindRecipeFromIngredients(ingredientInCrafting);
    }
}
