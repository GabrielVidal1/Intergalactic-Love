using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPile : Collectible
{
    public RandomItem[] items;

    public override void Interact(Player player)
    {

    }
}

[System.Serializable]
public class RandomItem
{
    public ItemData item;
    public float chance;
}