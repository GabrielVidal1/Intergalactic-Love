using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PartListItem : MonoBehaviour
{
    [SerializeField] private RawImage itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemQuantity;

    [SerializeField] private GameObject disabledOcclusion;

    [SerializeField] private PartListDraggable draggable;

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

    public SpaceshipPart part;

    public void Initialize(SpaceshipPart part, int quantity)
    {
        this.part = part;
        itemIcon.texture = part.itemData.texture;
        itemName.text = part.itemData.itemName;

        PreviewQuantity = quantity;

        draggable.Initialize(this);
    }

    public void SetDisable(bool disable)
    {
        disabledOcclusion.SetActive(disable);
    }
}
