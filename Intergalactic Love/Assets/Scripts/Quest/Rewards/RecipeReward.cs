using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeReward : QuestEvent
{
    [SerializeField] private Recipe[] unlockedRecipes;

    protected override IEnumerator Execute()
    {
        foreach (Recipe recipe in unlockedRecipes)
        {
            yield return StartCoroutine(GameManager.gm.recipeManager.DiscoverRecipe(recipe.index));
        }
    }
}
