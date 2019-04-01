using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystemUI : MonoBehaviour
{
    public RecipeList recipeList;
    public CraftingZone craftingZone;

    public List<ItemData> ingredientInCrafting;

    private PlayerInventory playerInventory;

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


}
