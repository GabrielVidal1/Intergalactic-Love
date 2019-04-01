using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InventoryListItem : MonoBehaviour
{
    [SerializeField] private RawImage itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemQuantity;

    [SerializeField] private InventoryListItemDraggable draggable;

    private ItemData itemData;

    public void Initialize(ItemData itemData, int quantity)
    {
        this.itemData = itemData;
        itemIcon.texture = itemData.texture;
        itemName.text = itemData.itemName;
        itemQuantity.text = quantity == 0 ? "" : quantity.ToString();
        draggable.itemIcon.texture = itemData.texture;

        draggable.Initialize(this);
    }

    public ItemData GetItemData()
    {
        return itemData;
    }
}
