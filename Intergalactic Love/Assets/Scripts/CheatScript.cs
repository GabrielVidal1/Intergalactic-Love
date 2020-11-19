using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour
{
    public Recipe[] recipeToDiscover;

    public ItemData[] itemsToGive;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Reset Inventory and recipes");

            GameManager.gm.player.playerInventory.inventory.Clear();
            GameManager.gm.recipeManager.ResetKnownRecipe();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Gave Items to player");

            foreach (ItemData item in itemsToGive)
            {
                GameManager.gm.player.playerInventory.AddItemToInventory(item, 20);
            }

            foreach (Recipe r in recipeToDiscover)
            {
                StartCoroutine(GameManager.gm.recipeManager.DiscoverRecipe(r.index));
            }
        }
    }
}
