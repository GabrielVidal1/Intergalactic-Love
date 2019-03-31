using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystemUI : MonoBehaviour
{
    public RecipeList recipeList;

    private PlayerInventory playerInventory;

    public void Initialize()
    {
        playerInventory = GameManager.gm.player.playerInventory;
    }

    public void OnOpenCraftingTab()
    {
        recipeList.Initialize(this);
    }

    public void OnClickRecipe(Recipe recipe)
    {
        if (CanCraftItem(recipe))
        {

        }
    }

    public bool CanCraftItem(Recipe recipe)
    {

        return true;
    }

}
