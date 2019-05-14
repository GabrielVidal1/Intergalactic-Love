using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NewRecipePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private RawImage recipeTexture;

    [SerializeField] private float delay = 1.5f;

    private CanvasGroup group;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0f;
    }


    public IEnumerator Trigger(Recipe recipe)
    {
        //Debug.Log("Triggered " + recipe.result.itemName);

        recipeName.text = recipe.result.itemName;
        recipeTexture.texture = recipe.result.texture;

        GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.newRecipe);

        group.alpha = 1f;

        float currentDelay = delay;

        while (currentDelay > 0f)
        {
            if (currentDelay < 1f)
            {
                group.alpha = currentDelay;
            }

            currentDelay -= Time.deltaTime;
            yield return 0;
        }

        group.alpha = 0f;
    }
}
