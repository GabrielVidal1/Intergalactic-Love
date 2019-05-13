using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NewRecipePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private RawImage recipeTexture;

    [SerializeField] private float delay;

    private CanvasGroup group;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0f;
    }


    public IEnumerator Trigger(Recipe recipe)
    {
        recipeName.text = recipe.result.itemName;
        recipeTexture.texture = recipe.result.texture;

        GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.newRecipe);

        group.alpha = 1f;

        while(delay > 0f)
        {
            if (delay < 1f)
            {
                group.alpha = delay;
            }

            delay -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        group.alpha = 0f;
    }
}
