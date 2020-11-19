using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingZone : MonoBehaviour
{
    private CraftingSystemUI craftingSystem;

    [SerializeField] private CraftingZoneSlot[] craftingZoneSlots;
    [SerializeField] private Button craftButton;

    [SerializeField] private RawImage resultRawImage;
    [SerializeField] private TextMeshProUGUI resultAmountText;

    [SerializeField] private Texture unknownObject;
    [SerializeField] private Texture emptySlot;

    [SerializeField] private CanvasGroup noRecipeAlert;

    private PlayerInventory playerInventory;
    private Recipe presetRecipe;

    public List<ItemData> ingredientInCrafting = new List<ItemData>();

    public void Initialize(CraftingSystemUI craftingSystemUi)
    {
        playerInventory = GameManager.gm.player.playerInventory;
        if (playerInventory.inventory == null)
            playerInventory.inventory = new Dictionary<ItemData, int>();
        
        noRecipeAlert.alpha = 0f;
        craftingSystem = craftingSystemUi;
    }
    
    public void Open()
    {
        craftButton.interactable = false;
        presetRecipe = null;
        ingredientInCrafting.Clear();

        resultRawImage.texture = emptySlot;
        resultAmountText.text = "";

        foreach (CraftingZoneSlot item in craftingZoneSlots)
        {
            item.Initialize(this);
        }
    }
    
    public void OnClickCraft()
    {
        GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.clickOnCraft);

        if (presetRecipe == null)
        {
            presetRecipe = GameManager.gm.recipeManager.FindRecipeFromIngredients(ingredientInCrafting);

            // If a recipe exists with this unknown combination of ingredients 
            if (presetRecipe == null)
            {
                GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.failedCraft);

                // Consume ingredients
                foreach (ItemData ingredients in ingredientInCrafting)
                    playerInventory.RemoveItemFromInventory(ingredients, 1);

                // Reload the inventory item list
                craftingSystem.inventoryList.Open();

                // Reset ingredients and recipe
                ingredientInCrafting.Clear();
                presetRecipe = null;

                // Reload the recipe list
                craftingSystem.recipeList.UpdateRecipeList();

                StartCoroutine(PrintNoRecipeAlert());
            }
        }
        else
        {
            GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.successfulCraft);

            // Consume ingredients
            foreach (ItemData ingredient in presetRecipe.ingredients)
                playerInventory.RemoveItemFromInventory(ingredient, 1);

            // Add result to player inventory
            playerInventory.AddItemToInventory(presetRecipe.result, presetRecipe.amount);

            // Reload the inventory item list
            craftingSystem.inventoryList.Open();

            // Reset ingredients and recipe
            ingredientInCrafting.Clear();
            presetRecipe = null;

            // Reload the recipe list
            craftingSystem.recipeList.UpdateRecipeList();

            // Refill the slots
            /*
            foreach (CraftingZoneSlot slot in craftingZoneSlots)
                slot.ReplaceItem();
            */

        }
        
        craftButton.interactable = false;
        foreach (CraftingZoneSlot slot in craftingZoneSlots)
        {
            slot.ResetIlitem();
            slot.ResetItem();
        }
    }

    public void UpdateCraftingSlot(ItemData newItem, ItemData oldItem)
    {
        if (oldItem != null)
            ingredientInCrafting.Remove(oldItem);
        if (newItem != null)
            ingredientInCrafting.Add(newItem);

        if (ingredientInCrafting.Count == 3)
        {
            craftButton.interactable = true;

            Recipe recipe = GameManager.gm.recipeManager.FindRecipeFromIngredients(ingredientInCrafting);

            if (recipe)
            {
                presetRecipe = recipe;

                resultRawImage.texture = recipe.result.texture;
                resultAmountText.text = recipe.amount == 1 ? "" : recipe.amount.ToString();
            }
            else
            {
                resultRawImage.texture = unknownObject;
                resultAmountText.text = "";
            }
        }
        else
        {
            resultRawImage.texture = emptySlot;
            resultAmountText.text = "";

            craftButton.interactable = false;

        }
    }

    // The player can craft the recipe here
    public void SetRecipe(Recipe recipe)
    {
        // Reset the quantity of for ingredients in inventory item list
        foreach (ItemData ingredient in recipe.ingredients)
            craftingSystem.inventoryList.listItems[ingredient].PreviewQuantity = GameManager.gm.player.playerInventory.inventory[ingredient];

        // Place the ingredients on the crafting slots
        for (int i = 0; i < 3; i++)
        {
            InventoryListItem inventoryListItem = craftingSystem.inventoryList.listItems[recipe.ingredients[i]];
            craftingZoneSlots[i].ReplaceItem(inventoryListItem);
        }

        // Display the result of the recipe
        resultRawImage.texture = recipe.result.texture;
        resultAmountText.text = recipe.amount == 1 ? "" : recipe.amount.ToString();

        presetRecipe = recipe;

        // enable the craft button
        craftButton.interactable = true;
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

}
