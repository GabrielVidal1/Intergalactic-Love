using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeReward : QuestEvent
{
    [SerializeField] private Recipe unlockedRecipe;

    protected override IEnumerator Execute()
    {
        yield return StartCoroutine(GameManager.gm.recipeManager.DiscoverRecipe(unlockedRecipe.index));
    }
}
