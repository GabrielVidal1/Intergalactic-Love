using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public InventoryUI inventoryUI;

    public CraftingSystemUI craftingSystem;

    public GameObject interactTooltip;

    private bool isInventoryDisplayed = false;
    public bool IsInventoryOpened()
    { return isInventoryDisplayed; }

    private bool isCraftingDisplayed = false;

    private void Start()
    {
        craftingSystem.Initialize();
    }

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
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCraftingDisplayed = !isCraftingDisplayed;
            craftingSystem.gameObject.SetActive(isCraftingDisplayed);

            if (isCraftingDisplayed)
            {
                craftingSystem.OnOpenCraftingTab();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCraftingDisplayed)
            {
                isCraftingDisplayed = false;
                craftingSystem.gameObject.SetActive(isCraftingDisplayed);
            }

            if (isInventoryDisplayed)
            {
                isInventoryDisplayed = false;
                inventoryUI.gameObject.SetActive(isInventoryDisplayed);
            }
        }
    }

    public void ShowInteractTooltip(bool enable)
    {
        interactTooltip.SetActive(enable);
    }

}
