using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CraftingStationItemUI : MonoBehaviour
{
    [SerializeField] private RawImage itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemQuantity;

    public void Initialize(ItemData itemData, int quantity)
    {
        itemIcon.texture = itemData.texture;
        itemName.text = itemData.itemName;
        itemQuantity.text = quantity.ToString();
    }
}
