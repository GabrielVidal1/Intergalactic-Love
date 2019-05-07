using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsReward : QuestEvent
{
    public ItemAndQuantity[] neededItems;

    protected override IEnumerator Execute()
    {
        PlayerInventory playerInventory = GameManager.gm.player.playerInventory;

        foreach (ItemAndQuantity item in neededItems)
        {
            playerInventory.AddItemToInventory(item.item, item.quantity);
        }

        yield break;
    }

    [System.Serializable]
    public class ItemAndQuantity
    {
        public ItemData item;
        public int quantity;
    }
}
