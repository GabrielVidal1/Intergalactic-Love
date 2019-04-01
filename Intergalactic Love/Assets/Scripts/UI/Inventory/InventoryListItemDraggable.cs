using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class InventoryListItemDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static ItemData draggedItem;

    [SerializeField] public RawImage itemIcon;

    private InventoryListItem inventoryListItem;
    private CanvasGroup canvasGroup;

    public void Initialize(InventoryListItem inventoryListItem)
    {
        this.inventoryListItem = inventoryListItem;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(GameManager.gm.mainCanvas.transform);
        draggedItem = inventoryListItem.GetItemData();

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        transform.SetParent(inventoryListItem.transform);
        transform.localPosition = Vector3.zero;
        draggedItem = null;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 0.0f;
    }

}
