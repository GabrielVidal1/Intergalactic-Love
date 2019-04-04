using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class InventoryListItemDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static InventoryListItemDraggable draggedItem;

    [SerializeField] public RawImage itemIcon;

    [HideInInspector]
    public InventoryListItem inventoryListItem;
    private CanvasGroup canvasGroup;

    public bool shouldResetPreviewQuantity = true;

    public void Initialize(InventoryListItem inventoryListItem)
    {
        this.inventoryListItem = inventoryListItem;
        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0.0f;
        canvasGroup.blocksRaycasts = true;

        isDragged = false;
    }

    private bool isDragged;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventoryListItem.PreviewQuantity > 0)
        {
            transform.SetParent(GameManager.gm.mainCanvas.transform);
            draggedItem = this;

            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.6f;

            inventoryListItem.PreviewQuantity--;

            isDragged = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragged)
        {
            //Debug.Log("OnEndDrag");

            transform.SetParent(inventoryListItem.transform);
            transform.localPosition = Vector3.zero;
            draggedItem = null;

            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 0.0f;

            inventoryListItem.PreviewQuantity++;

            isDragged = false;
        }
    }
}
