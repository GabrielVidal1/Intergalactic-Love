﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public CraftingSystemUI craftingSystem;
    public DialogueSystem dialogueSystem;

    public GameObject interactTooltip;

    private bool isInventoryDisplayed = false;
    public bool IsInventoryOpened()
    { return isInventoryDisplayed; }

    private bool isCraftingDisplayed = false;
    public bool IsCraftingDisplayed()
    { return isCraftingDisplayed; }

    private void Start()
    {
        craftingSystem.Initialize();

        inventoryUI.gameObject.SetActive(false);
        craftingSystem.gameObject.SetActive(false);
        dialogueSystem.Initialize();
    }

    public void Update()
    {
        if (!dialogueSystem.IsExecutingDialogue())
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                isInventoryDisplayed = !isInventoryDisplayed;
                inventoryUI.gameObject.SetActive(isInventoryDisplayed);

                if (isInventoryDisplayed)
                {
                    inventoryUI.Initialize();
                }
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                isCraftingDisplayed = !isCraftingDisplayed;
                craftingSystem.gameObject.SetActive(isCraftingDisplayed);

                inventoryUI.Initialize();

                if (isCraftingDisplayed)
                {
                    craftingSystem.OnOpenCraftingTab();
                }
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
