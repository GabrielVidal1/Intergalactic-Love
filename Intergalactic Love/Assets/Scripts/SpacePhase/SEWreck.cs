using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEWreck : MonoBehaviour
{
    [SerializeField] private ItemAndChance[] collectibleItems;

    [System.Serializable]
    public class ItemAndChance
    {
        public ItemData item;
        public int quantity;

        public float chance;
    }

    public void CollectItems()
    {
        PlayerInventory playerInventory = GameManager.gm.player.playerInventory;

        for (int i = 0; i < collectibleItems.Length; i++)
        {
            if (collectibleItems[i].chance > Random.value)
            {
                playerInventory.AddItemToInventory(
                collectibleItems[i].item,
                collectibleItems[i].quantity);
            }
        }

        Destroy(gameObject);
    }
}
