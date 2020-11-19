using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeList : MonoBehaviour
{
    [SerializeField] private RecipeListItem recipeListItemPrefab;
    [SerializeField] private Transform recipeListParent;

    private CraftingSystemUI craftingSystem;
    
    private readonly List<RecipeListItem> recipeListItems = new List<RecipeListItem>();
    private RecipeManager recipeManager;

    public void Initialize(CraftingSystemUI craftingSystemUi)
    {
        RecipeListItem.RecipeList = this;
        craftingSystem = craftingSystemUi;
        recipeManager = GameManager.gm.recipeManager;
    }
    
    public void Open()
    {
        foreach (RecipeListItem recipeListItem in recipeListItems)
            Destroy(recipeListItem.gameObject);
        recipeListItems.Clear();

        for (int i = 0; i < recipeManager.hasDiscoveredRecipe.Length; i++)
        {
            if (recipeManager.hasDiscoveredRecipe[i])
            {
                RecipeListItem recipeListItem = Instantiate(recipeListItemPrefab, recipeListParent);
                recipeListItem.Initialize(recipeManager.recipes[i]);
                recipeListItems.Add(recipeListItem);
            }
        }
    }

    public void UpdateRecipeList()
    {
        foreach (RecipeListItem recipeListItem in recipeListItems)
            recipeListItem.UpdateStatus();
    }

    // Player can craft it
    public void OnClickRecipe(Recipe recipe)
    {
        GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.clickOnRecipe);
        craftingSystem.craftingZone.SetRecipe(recipe);
    }
}
