using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopupParent : MonoBehaviour
{
    [SerializeField] private ItemPopup itemPopupPrefab;

    private Dictionary<ItemData, ItemPopup> itemPopups;

    [SerializeField] private Transform parent;

    public void Initialize()
    {
        itemPopups = new Dictionary<ItemData, ItemPopup>();
    }

    public void AddItem(ItemData itemData)
    {
        if (itemPopups.ContainsKey(itemData))
        {
            itemPopups[itemData].AddItem();
        }
        else
        {
            itemPopups[itemData] = Instantiate(itemPopupPrefab, parent);
            itemPopups[itemData].Create(this, itemData);
        }
    }

    public void DestroyPopup(ItemData itemData)
    {
        itemPopups.Remove(itemData);
    }
}
