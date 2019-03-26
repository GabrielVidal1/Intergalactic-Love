using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPile : Interactible
{
    public RandomItem[] items;

    public override void Interact(Player player)
    {

    }

    public void SpawnItems()
    {

    }
}

[System.Serializable]
public class RandomItem
{
    public ItemData item;
    public float chance;
}