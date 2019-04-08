using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class PartListDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static PartListDraggable draggedItem;

    [HideInInspector]
    public PartListItem partListItem;

    public bool shouldResetPreviewQuantity = true;

    public void Initialize(PartListItem partListItem)
    {
        this.partListItem = partListItem;
        isDragged = false;
    }

    private bool isDragged;

    private SpaceshipPart spaceshipPart;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (partListItem.PreviewQuantity > 0)
        {
            transform.SetParent(GameManager.gm.mainCanvasSE.transform);
            draggedItem = this;

            spaceshipPart = Instantiate(partListItem.part);
            spaceshipPart.Initialize();
            spaceshipPart.DisableCollider(true);
            partListItem.PreviewQuantity--;
            isDragged = true;
        }
    }

    private SpaceshipPart parentPart;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        Ray ray = GameManager.gm.mainCanvasSE.GetRayFromMousePosition();
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            bool isPosValid = false;
            if (hit.collider.CompareTag("SpaceshipPart") &&
                hit.collider.GetComponent<SpaceshipPartMesh>().GetPart().canBeParent)
            {
                spaceshipPart.SetMat(null);
                parentPart = hit.collider.GetComponent<SpaceshipPartMesh>().GetPart();
                isPosValid = true;
            }
            else
            {
                spaceshipPart.SetMat(GameManager.gm.mainCanvasSE.notValidMat);
                parentPart = null;
            }

            spaceshipPart.DisableSymetry(isPosValid);
            spaceshipPart.SetPosition(hit.point, isPosValid);

            Vector3 normal = -hit.normal;
            normal.x = -Mathf.Abs(normal.x);
            spaceshipPart.SetRotation(Quaternion.LookRotation(normal));
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragged)
        {
            if (parentPart != null)
            {
                spaceshipPart.AttachToPart(parentPart);
                partListItem.PreviewQuantity--;
            }
            else
            {
                Destroy(spaceshipPart.gameObject);
            }
            spaceshipPart = null;

            transform.SetParent(partListItem.transform);
            transform.localPosition = Vector3.zero;
            draggedItem = null;

            partListItem.PreviewQuantity++;
            isDragged = false;
            parentPart = null;
        }
    }
}