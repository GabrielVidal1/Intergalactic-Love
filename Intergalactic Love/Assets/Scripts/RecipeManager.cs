using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public CraftingStationIcons craftingStationIconsPrefab;

    public Material craftingStationIconIsUsed;
    public Material craftingStationIconCanUse;
    public Material craftingStationIconCantUse;

    [Header("Recipes")]
    public Recipe[] recipes;
    public bool[] hasDiscoveredRecipe;

    public IEnumerator DiscoverRecipe(int index)
    {
        if (!hasDiscoveredRecipe[index])
        {
            yield return StartCoroutine(GameManager.gm.mainCanvas.newRecipePopup.Trigger(recipes[index]));
        }

        hasDiscoveredRecipe[index] = true;
    }

    public void ResetKnownRecipe()
    {
        hasDiscoveredRecipe = new bool[recipes.Length];
        for (int i = 0; i < recipes.Length; i++)
        {
            hasDiscoveredRecipe[i] = false;
            recipes[i].index = i;
        }
    }

    public void Initialize()
    {
        ResetKnownRecipe();
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

    /*
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
    */

    public Recipe FindRecipeFromIngredients(List<ItemData> ingredients)
    {
        HashSet<ItemData> second = new HashSet<ItemData>(ingredients);
        foreach (Recipe recipe in recipes)
        {
            if (new HashSet<ItemData>(recipe.ingredients).SetEquals(second))
                return recipe;
        }
        return null;
    }
}
