﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public Recipe[] recipes;
    [HideInInspector]
    public bool[] hasDiscoveredRecipe;

    public void DiscoverRecipe(int index)
    {
        hasDiscoveredRecipe[index] = true;
    }

    public void Initialize()
    {
        Debug.Log(recipes.Length);

        hasDiscoveredRecipe = new bool[recipes.Length];
        for (int i = 0; i < recipes.Length; i++)
            hasDiscoveredRecipe[i] = false;
    }

    public void DiscoverRandomRecipe()
    {
        Debug.Log("DiscoverRandomRecipe");

        int i = 0;
        while (i < hasDiscoveredRecipe.Length && hasDiscoveredRecipe[i])
            i++;
        Debug.Log(i);
        if (!(i < hasDiscoveredRecipe.Length))
            return;

        DiscoverRecipe(i);
        SaveLoad.SaveGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DiscoverRandomRecipe();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < hasDiscoveredRecipe.Length; i++)
            {
                Debug.Log(recipes[i].result.itemName + ": " + (hasDiscoveredRecipe[i] ? "" : "not") + " discovered");
            }
        }
    }
}
