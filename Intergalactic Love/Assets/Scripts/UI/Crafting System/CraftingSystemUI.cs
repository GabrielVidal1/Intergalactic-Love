using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystemUI : MonoBehaviour
{
    public RecipeList recipeList;
    public CraftingZone craftingZone;

    public List<ItemData> ingredientInCrafting;

    [SerializeField] private CanvasGroup noRecipeAlert;

    private PlayerInventory playerInventory;

    private Recipe presetRecipe;
    public InventoryList inventoryList;

    public void Initialize()
    {
        inventoryList = GameManager.gm.mainCanvas.inventoryUI.inventoryList;
        playerInventory = GameManager.gm.player.playerInventory;
        noRecipeAlert.alpha = 0f;
    }

    public void OnOpenCraftingTab()
    {
        craftingZone.Initialize(this);
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

    public bool Craft()
    {
        if (presetRecipe == null)
        {
            presetRecipe = FindAssociatedRecipe();
            if (presetRecipe == null)
            {
                //DESTROY INGREDIENTS

                for (int i = 0; i < ingredientInCrafting.Count; i++)
                {
                    playerInventory.RemoveItemFromInventory(ingredientInCrafting[i], 1);
                    inventoryList.listItems[ingredientInCrafting[i]].ResetPreviewQuantity();
                }

                StartCoroutine(PrintNoRecipeAlert());

                Debug.Log("NO RECIPE");
                return false;
            }
        }

        IEnumerator PrintNoRecipeAlert()
        {
            noRecipeAlert.alpha = 1f;
            yield return new WaitForSecondsRealtime(1f);

            for (float i = 1f; i >= 0f; i-=0.02f)
            {
                noRecipeAlert.alpha = i;
                yield return 0;
            }
            noRecipeAlert.alpha = 0f;
        }


        foreach (ItemData ingredient in presetRecipe.ingredients)
        {
            playerInventory.RemoveItemFromInventory(ingredient, 1);
            inventoryList.listItems[ingredient].ResetPreviewQuantity();
        }

        playerInventory.AddItemToInventory(presetRecipe.result, presetRecipe.amount);
        if (!inventoryList.listItems.ContainsKey(presetRecipe.result))
        {
            inventoryList.AddNewItem(presetRecipe.result);
        }

        inventoryList.listItems[presetRecipe.result].ResetPreviewQuantity();

        //reset ingredients and recipe
        ingredientInCrafting = new List<ItemData>();
        presetRecipe = null;

        return true;
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
