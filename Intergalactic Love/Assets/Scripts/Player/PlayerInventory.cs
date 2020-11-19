using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<ItemData, int> inventory;

    private Player player;

    public void Initialize(Player player)
    {
        inventory = new Dictionary<ItemData, int>();
        this.player = player;
    }

    public void AddItemToInventory(ItemData item, int amount)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] += amount;
        }
        else
        {
            inventory[item] = amount;
        }

        GameManager.gm.questManager.UpdateQuestNPC(Quest.ValidatorType.GatherItems);
        GameManager.gm.questManager.UpdateQuestNPC(Quest.ValidatorType.HasItems);
        GameManager.gm.questManager.UpdateQuestNPC(Quest.ValidatorType.ConsumeItems);

        for (int i = 0; i < amount; i++)
        {
            GameManager.gm.mainCanvas.itemPopupParent.AddItem(item);
        }
    }

    public bool RemoveItemFromInventory(ItemData item, int amount)
    {
        if (!inventory.ContainsKey(item))
            return false;
        if (inventory[item] < amount)
            return false;

        inventory[item] -= amount;
        GameManager.gm.questManager.UpdateQuestNPC(Quest.ValidatorType.ConsumeItems);
        GameManager.gm.questManager.UpdateQuestNPC(Quest.ValidatorType.HasItems);
        return true;
    }

    public bool Collect(Collectible collectible)
    {
        switch (collectible)
        {
            case DroppedItem droppedItem:
                AddItemToInventory(droppedItem.associatedItem, 1);
                break;
            case Blueprint blueprint:
                GameManager.gm.recipeManager.DiscoverRecipe(blueprint.associatedRecipe.index);
                break;
        }

        return true;
    }
    
    public bool CanCraftItem(Recipe recipe)
    {
        Dictionary<ItemData, int> ingredients = new Dictionary<ItemData, int>();

        foreach (ItemData ing in recipe.ingredients)
            ingredients[ing] = ingredients.ContainsKey(ing) ? ingredients[ing] + 1 : 1;

        foreach (KeyValuePair<ItemData, int> val in ingredients)
        {
            if (!inventory.ContainsKey(val.Key))
                return false;
            if (inventory[val.Key] < val.Value)
                return false;
        }
        return true;
    }

}
