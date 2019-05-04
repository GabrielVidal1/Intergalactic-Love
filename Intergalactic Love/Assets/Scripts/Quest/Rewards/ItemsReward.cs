using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsReward : QuestReward
{
    public ItemAndQuantity[] neededItems;

    public override void RewardPlayer()
    {
        PlayerInventory playerInventory = GameManager.gm.player.playerInventory;

        foreach (ItemAndQuantity item in neededItems)
        {
            playerInventory.AddItemToInventory(item.item, item.quantity);
        }
    }

    [System.Serializable]
    public class ItemAndQuantity
    {
        public ItemData item;
        public int quantity;
    }
}
