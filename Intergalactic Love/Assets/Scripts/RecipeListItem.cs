using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class RecipeListItem : MonoBehaviour
{
    public static RecipeList recipeList;

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private RawImage itemTexture;
    [SerializeField] private TextMeshProUGUI recipeAmountText;

    [SerializeField] private RawImage[] ingredientTextures;

    private Recipe recipe;

    public void Initialize(Recipe recipe)
    {
        this.recipe = recipe;

        recipeNameText.text = recipe.result.itemName;
        recipeAmountText.text = recipe.amount == 1 ? "" : recipe.amount.ToString();
        itemTexture.texture = recipe.result.texture;

        int i = 0;
        for (; i < recipe.ingredients.Length; i++)
        {
            ingredientTextures[i].texture = recipe.ingredients[i].texture;
        }
        for (; i < ingredientTextures.Length; i++)
        {
            ingredientTextures[i].gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        recipeList.OnClickRecipe(recipe);
    }
}
