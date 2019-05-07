using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class CraftingZoneSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private RawImage ingredientTexture;
    [SerializeField] private GameObject removeButton;

    [SerializeField] private Texture emptySlotTexture;

    public ItemData item;

    private CraftingZone craftingZone;

    private InventoryListItem ilitem;

    public void Initialize(CraftingZone craftingZone)
    {
        this.craftingZone = craftingZone;
        ResetItem();
    }

    public void SetIngredient(ItemData item)
    {
        craftingZone.UpdateCraftingSlot(item, this.item);
        this.item = item;
        ingredientTexture.texture = item.texture;
        removeButton.SetActive(true);

    }

    public void ResetItem()
    {
        removeButton.SetActive(false);
        ingredientTexture.texture = emptySlotTexture;
        craftingZone.UpdateCraftingSlot(null, item);
        item = null;

        if (ilitem != null)
            ilitem.PreviewQuantity++;
        ilitem = null;
    }

    public void ResetIlitem()
    {
        ilitem = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (InventoryListItemDraggable.draggedItem != null)
        {
            if (ilitem != null)
            {
                ilitem.PreviewQuantity++;
            }

            GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.dropItemFromInventory);

            ilitem = InventoryListItemDraggable.draggedItem.inventoryListItem;

            ilitem.PreviewQuantity--;

            SetIngredient(ilitem.GetItemData());
        }
    }

    public void ReplaceItem()
    {
        item = null;
        if (ilitem == null)
        {
            Debug.Log("ERROR");
            return;
        }

        if (ilitem.PreviewQuantity <= 0)
        {
            ilitem = null;
            ResetItem();
            //return false;
        }
        else
        {
            //print("Item is correctly replaced : " + ilitem.PreviewQuantity + "remainning");

            SetIngredient(ilitem.GetItemData());
            ilitem.PreviewQuantity--;
            //return true;
        }


    }

    public void ReplaceItem(InventoryListItem ilitem)
    {
        this.ilitem = ilitem;
        ReplaceItem();
    }

}
