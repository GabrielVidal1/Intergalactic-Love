using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList : MonoBehaviour
{
    public InventoryListItem inventoryListItemPrefab;
    public Transform listItemParent;

    public Dictionary<ItemData, InventoryListItem> listItems;

    public void Initialize()
    {
        for (int i = 0; i < listItemParent.childCount; i++)
            Destroy(listItemParent.GetChild(i).gameObject);

        listItems = new Dictionary<ItemData, InventoryListItem>();

        foreach (KeyValuePair<ItemData, int> item in GameManager.gm.player.playerInventory.inventory)
        {
            InventoryListItem r = Instantiate(inventoryListItemPrefab, listItemParent);
            r.Initialize(item.Key, item.Value);

            listItems[item.Key] = r;
        }
    }

    public void AddNewItem(ItemData item)
    {
        InventoryListItem r = Instantiate(inventoryListItemPrefab, listItemParent);
        r.Initialize(item, GameManager.gm.player.playerInventory.inventory[item]);
        listItems[item] = r;
    }

}
