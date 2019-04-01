using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class CraftingZoneSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private RawImage ingredientTexture;
    [SerializeField] private GameObject removeButton;

    public ItemData item;

    private CraftingZone craftingZone;

    public void Initialize(CraftingZone craftingZone)
    {
        this.craftingZone = craftingZone;
        removeButton.SetActive(false);
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
        ingredientTexture.texture = null;
        craftingZone.UpdateCraftingSlot(null, item);
        item = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (InventoryListItemDraggable.draggedItem != null)
        {
            SetIngredient(InventoryListItemDraggable.draggedItem);
        }
    }
}
