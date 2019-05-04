﻿using System.Collections;
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
        if (collectible is DroppedItem)
        {
            DroppedItem droppedItem = (DroppedItem)collectible;

            AddItemToInventory(droppedItem.associatedItem, 1);

        }

        return true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Interactible.targetedItem != null)
            {
                Interactible.targetedItem.Interact(player);
            }
        }
    }
}
