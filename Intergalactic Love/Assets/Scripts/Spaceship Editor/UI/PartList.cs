using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartList : MonoBehaviour
{
    public Transform listPartParent;
    public PartListItem listPartPrefab;

    public Dictionary<ItemData, PartListItem> listParts;

    public void Initialize()
    {
        for (int i = 0; i < listPartParent.childCount; i++)
            Destroy(listPartParent.GetChild(i).gameObject);

        listParts = new Dictionary<ItemData, PartListItem>();

        foreach (KeyValuePair<ItemData, int> item in 
            GameManager.gm.player.playerInventory.inventory)
        {
            if (GameManager.gm.itemManager.itemToPart.ContainsKey(item.Key))
            {
                AddListPart(item.Key, item.Value);
            }
        }
    }

    public void AddListPart(ItemData item, int amount)
    {
        PartListItem r = Instantiate(listPartPrefab, listPartParent);
        r.Initialize(GameManager.gm.itemManager.itemToPart[item], amount);

        listParts[item] = r;
    }
}
