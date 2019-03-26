using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<ItemData, int> inventory;

    public float maxMass;

    public void Initialize()
    {
        inventory = new Dictionary<ItemData, int>();
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




        return true;
    }

}
