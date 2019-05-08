using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeList : MonoBehaviour
{
    [SerializeField] private RecipeListItem recipeListItemPrefab;
    [SerializeField] private Transform recipeListParent;

    private CraftingSystemUI craftingSystem;

    public void Initialize(CraftingSystemUI craftingSystem)
    {
        RecipeListItem.recipeList = this;

        this.craftingSystem = craftingSystem;

        RecipeManager recipeManager = GameManager.gm.recipeManager;

        for (int i = 0; i < recipeListParent.childCount; i++)
            Destroy(recipeListParent.GetChild(i).gameObject);

        for (int i = 0; i < recipeManager.hasDiscoveredRecipe.Length; i++)
        {
            if (recipeManager.hasDiscoveredRecipe[i])
            {
                RecipeListItem recipeListItem = Instantiate(recipeListItemPrefab, recipeListParent);
                recipeListItem.Initialize(recipeManager.recipes[i], craftingSystem);
            }
        }
    }

    public void UpdateRecipeList()
    {
        for (int i = 0; i < recipeListParent.childCount; i++)
        {
            RecipeListItem recipeListItem = recipeListParent.GetChild(i).GetComponent<RecipeListItem>();
            recipeListItem.UpdateStatus();
        }
    }

    public void OnClickRecipe(Recipe recipe)
    {
        GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.clickOnRecipe);

        craftingSystem.OnClickRecipe(recipe);
    }
}
