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
                PartListItem r = Instantiate(listPartPrefab, listPartParent);
                r.Initialize(GameManager.gm.itemManager.itemToPart[item.Key], item.Value);

                listParts[item.Key] = r;
            }
        }
    }
}
