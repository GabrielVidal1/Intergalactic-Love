using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<ItemData> items;
    public List<SpaceshipPart> parts;

    public Dictionary<ItemData, SpaceshipPart> itemToPart;

    public void Initialize()
    {
        itemToPart = new Dictionary<ItemData, SpaceshipPart>();

        foreach (SpaceshipPart part in parts)
        {
            itemToPart[part.itemData] = part;
            //items.Add(part.itemData);
        }

        for (int i = 0; i < items.Count; i++)
        {
            items[i].index = i;
        }

        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].index = i;
        }
    }
}
