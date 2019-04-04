using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingZone : MonoBehaviour
{
    private CraftingSystemUI craftingSystemUI;

    [SerializeField] private CraftingZoneSlot[] craftingZoneSlots;
    [SerializeField] private Button craftButton;

    [SerializeField] private RawImage resultRawImage;
    [SerializeField] private TextMeshProUGUI resultAmountText;

    [SerializeField] private Texture unknownObject;
    [SerializeField] private Texture emptySlot;


    public void Initialize(CraftingSystemUI craftingSystemUI)
    {
        this.craftingSystemUI = craftingSystemUI;
        craftButton.interactable = false;

        resultRawImage.texture = emptySlot;
        resultAmountText.text = "";

        foreach (CraftingZoneSlot item in craftingZoneSlots)
        {
            item.Initialize(this);
        }
    }

    public void OnClickCraft()
    {
        bool consumeItems = craftingSystemUI.Craft();

        if (consumeItems)
        {
            foreach (CraftingZoneSlot slot in craftingZoneSlots)
            {
                slot.ReplaceItem();
            }
        } else
        {
            foreach (CraftingZoneSlot slot in craftingZoneSlots)
            {
                slot.ResetIlitem();
                slot.ResetItem();
            }
        }
    }

    public void UpdateCraftingSlot(ItemData newItem, ItemData oldItem)
    {
        if (oldItem != null)
            craftingSystemUI.ingredientInCrafting.Remove(oldItem);
        if (newItem != null)
            craftingSystemUI.ingredientInCrafting.Add(newItem);

        if (craftingSystemUI.ingredientInCrafting.Count == 3)
        {
            craftButton.interactable = true;

            Recipe recipe = GameManager.gm.recipeManager.FindRecipeFromIngredients(craftingSystemUI.ingredientInCrafting);

            if (recipe)
            {
                craftingSystemUI.PresetRecipe(recipe);

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

    public void SetRecipe(Recipe recipe)
    {
        foreach (ItemData ingredient in recipe.ingredients)
        {
            craftingSystemUI.inventoryList.listItems[ingredient].ResetPreviewQuantity();
        }

        for (int i = 0; i < craftingZoneSlots.Length; i++)
        {
            InventoryListItem ilim = craftingSystemUI.inventoryList.listItems[recipe.ingredients[i]];
            craftingZoneSlots[i].ReplaceItem(ilim);
        }

        resultRawImage.texture = recipe.result.texture;
        resultAmountText.text = recipe.amount == 1 ? "" : recipe.amount.ToString();

        craftingSystemUI.PresetRecipe(recipe);
    }
}
