using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList : MonoBehaviour
{
    public InventoryListItem inventoryListItemPrefab;
    public Transform listItemParent;

    public void OnOpenInventory()
    {
        for (int i = 0; i < listItemParent.childCount; i++)
            Destroy(listItemParent.GetChild(i).gameObject);

        foreach (KeyValuePair<ItemData, int> item in GameManager.gm.player.playerInventory.inventory)
        {
            InventoryListItem r = Instantiate(inventoryListItemPrefab, listItemParent);
            r.Initialize(item.Key, item.Value);
        }

    }

}
