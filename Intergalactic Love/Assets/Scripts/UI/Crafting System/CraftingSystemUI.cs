using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystemUI : MonoBehaviour
{
    public RecipeList recipeList;
    public CraftingZone craftingZone;
    public InventoryList inventoryList;

    public void Initialize()
    {
        inventoryList = GameManager.gm.mainCanvas.inventoryUI.inventoryList;
        
        recipeList.Initialize(this);
        craftingZone.Initialize(this);
    }

    public void OnOpenCraftingTab()
    {
        craftingZone.Open();
        recipeList.Open();
    }

    public void OnClickRecipe(Recipe recipe)
    {
        craftingZone.SetRecipe(recipe);
    }
}
