using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<ItemData, int> inventory;

    public float maxMass;

    private Player player;

    public void Initialize(Player player)
    {
        inventory = new Dictionary<ItemData, int>();
        this.player = player;
    }

    public float CarriedMass()
    {
        float total = 0f;

        foreach (KeyValuePair<ItemData, int> item in inventory)
            total += item.Key.mass * item.Value;

        return total;
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
