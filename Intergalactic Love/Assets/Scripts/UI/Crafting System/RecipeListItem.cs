using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class RecipeListItem : MonoBehaviour
{
    public static RecipeList RecipeList;

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private RawImage itemTexture;
    [SerializeField] private TextMeshProUGUI recipeAmountText;

    [SerializeField] private RawImage[] ingredientTextures;

    [SerializeField] private GameObject disable;

    private Recipe recipe;
    private bool canDo;


    public void Initialize(Recipe r)
    {
        recipe = r;

        recipeNameText.text = recipe.result.itemName;
        recipeAmountText.text = recipe.amount == 1 ? "" : recipe.amount.ToString();
        itemTexture.texture = recipe.result.texture;

        int i = 0;
        for (; i < recipe.ingredients.Length; i++)
            ingredientTextures[i].texture = recipe.ingredients[i].texture;
        for (; i < ingredientTextures.Length; i++)
            ingredientTextures[i].gameObject.SetActive(false);

        UpdateStatus();
    }

    public void OnClick()
    {
        GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.cantClickHere);

        if (canDo)
            RecipeList.OnClickRecipe(recipe);
    }

    public void UpdateStatus()
    {
        canDo = GameManager.gm.player.playerInventory.CanCraftItem(recipe);

        disable.SetActive(!canDo);
    }
}
