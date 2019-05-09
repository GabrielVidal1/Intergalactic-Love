using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeReward : QuestEvent
{
    [SerializeField] private Recipe unlockedRecipe;

    protected override IEnumerator Execute()
    {
        GameManager.gm.recipeManager.DiscoverRecipe(unlockedRecipe.index);
        yield break;
    }
}
