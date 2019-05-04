using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasItemValidator : QuestValidator
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
        return Quest.ValidatorType.HasItems;
    }

    public override void ValidatePart() { }

    [System.Serializable]
    public class ItemAndQuantity
    {
        public ItemData item;
        public int quantity;
    }
}
