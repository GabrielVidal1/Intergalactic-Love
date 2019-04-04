using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<ItemData> items;

    private void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].index = i;
        }
    }
}
