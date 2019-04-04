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

    [SerializeField] private GameObject disabledOcclusion;

    private ItemData itemData;

    private int previewQuantity;
    public int PreviewQuantity
    {
        get { return previewQuantity; }
        set
        {
            //Debug.Log("delta =" + (value - previewQuantity).ToString());
            previewQuantity = value;
            itemQuantity.text = previewQuantity <= 1 ? "" : previewQuantity.ToString();
            SetDisable(previewQuantity == 0);
        }
    }

    public void ResetPreviewQuantity()
    {
        PreviewQuantity = GameManager.gm.player.playerInventory.inventory[itemData];
    }

    public void Initialize(ItemData itemData, int quantity)
    {
        //Debug.Log(itemData.name + " = " + quantity.ToString());


        this.itemData = itemData;
        itemIcon.texture = itemData.texture;
        itemName.text = itemData.itemName;
        PreviewQuantity = quantity;
        draggable.itemIcon.texture = itemData.texture;

        draggable.Initialize(this);
    }

    public ItemData GetItemData()
    {
        return itemData;
    }

    public void SetDisable(bool disable)
    {
        disabledOcclusion.SetActive(disable);
    }
}
