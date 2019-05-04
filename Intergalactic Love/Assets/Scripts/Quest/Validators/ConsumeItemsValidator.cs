using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConsumeItemsValidator : QuestValidator
{
    public ItemAndQuantity[] neededItems;

    public override bool CanPartBeValidated()
    {
        PlayerInventory playerInventory = GameManager.gm.player.playerInventory;

        foreach (ItemAndQuantity item in neededItems)
        {
            if (!playerInventory.inventory.ContainsKey(item.item)) return false;
            if (playerInventory.inventory[item.item] < item.quantity) return false;
        }
        return true;
    }

    public override Quest.ValidatorType GetValidatorType()
    {
        return Quest.ValidatorType.ConsumeItems;
    }

    public override void ValidatePart()
    {
        if (CanPartBeValidated())
        {
            PlayerInventory playerInventory = GameManager.gm.player.playerInventory;

            foreach (ItemAndQuantity item in neededItems)
            {
                playerInventory.RemoveItemFromInventory(item.item, item.quantity);
            }
        }
    }

    [System.Serializable]
    public class ItemAndQuantity
    {
        public ItemData item;
        public int quantity;
    }
}
