using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ItemPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private RawImage texture;

    [SerializeField] private CanvasGroup group;

    [SerializeField] private float delay;

    private int amount;

    private float destroyInTime;
    private ItemPopupParent ipp;

    private ItemData itemData;

    public void Create(ItemPopupParent ipp, ItemData itemData)
    {
        this.ipp = ipp;
        this.itemData = itemData;
        amount = 1;

        texture.texture = itemData.texture;
        itemNameText.text = itemData.itemName;
        amountText.text = amount.ToString();

        destroyInTime = delay;

        StartCoroutine(Disappear());
    }

    public void AddItem()
    {
        amount++;
        amountText.text = amount.ToString();

        destroyInTime = delay;
    }

    IEnumerator Disappear()
    {
        while (destroyInTime > 0f)
        {
            if (destroyInTime <= 1f)
            {
                group.alpha = destroyInTime;
            }

            destroyInTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        ipp.DestroyPopup(itemData);
        Destroy(gameObject);
    }
}
