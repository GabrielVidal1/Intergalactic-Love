using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingZone : MonoBehaviour
{
    private CraftingSystemUI craftingSystemUI;

    [SerializeField] private CraftingZoneSlot[] craftingZoneSlots;
    [SerializeField] private Button craftButton;

    public void Initialize(CraftingSystemUI craftingSystemUI)
    {
        this.craftingSystemUI = craftingSystemUI;
        craftButton.interactable = false;

        foreach (CraftingZoneSlot item in craftingZoneSlots)
        {
            item.Initialize(this);
        }
    }

    public void OnClickCraft()
    {

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
        }
    }

    public void SetRecipe(Recipe recipe)
    {
        for (int i = 0; i < craftingZoneSlots.Length; i++)
        {
            craftingZoneSlots[i].SetIngredient(recipe.ingredients[i]);
        }
    }
}
