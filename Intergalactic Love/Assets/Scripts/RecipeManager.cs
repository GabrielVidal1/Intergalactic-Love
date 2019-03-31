using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public Recipe[] recipes;
    public bool[] hasDiscoveredRecipe;

    public void DiscoverRecipe(int index)
    {

    }

    public void Initialize()
    {
        hasDiscoveredRecipe = new bool[recipes.Length];
        for (int i = 0; i < hasDiscoveredRecipe.Length; i++)
            hasDiscoveredRecipe[i] = false;
    }

}
