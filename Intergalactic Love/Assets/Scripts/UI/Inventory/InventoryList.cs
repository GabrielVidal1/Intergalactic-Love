using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList : MonoBehaviour
{
    public InventoryListItem inventoryListItemPrefab;
    public Transform listItemParent;

    public Dictionary<ItemData, InventoryListItem> listItems = new Dictionary<ItemData, InventoryListItem>();

    public void Open()
    {
        for (int i = 0; i < listItemParent.childCount; i++)
            Destroy(listItemParent.GetChild(i).gameObject);

        listItems.Clear();

        foreach (KeyValuePair<ItemData, int> item in GameManager.gm.player.playerInventory.inventory)
            AddItem(item.Key, item.Value);
    }

    private void AddItem(ItemData item, int amount)
    {
        InventoryListItem r = Instantiate(inventoryListItemPrefab, listItemParent);
        r.Initialize(item, amount);
        listItems[item] = r;
    }

}
