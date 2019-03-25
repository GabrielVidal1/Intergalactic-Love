using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectibleItem : Collectible
{
    public ItemData associatedItem;
    public int amount;

    public override void Interact(Player player)
    {
        player.playerInventory.AddItemToInventory(associatedItem, amount);
    }
}
