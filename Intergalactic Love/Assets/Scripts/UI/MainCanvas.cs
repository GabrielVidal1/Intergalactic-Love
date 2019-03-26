using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public GameObject interactTooltip;

    private bool isInventoryDisplayed = false;
    public bool IsInventoryOpened()
    { return isInventoryDisplayed; }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryDisplayed = !isInventoryDisplayed;

            inventoryUI.gameObject.SetActive(isInventoryDisplayed);

            if (isInventoryDisplayed)
            {
                inventoryUI.OnOpenInventory();
            }
        }
    }

    public void ShowInteractTooltip(bool enable)
    {
        interactTooltip.SetActive(enable);
    }

}
