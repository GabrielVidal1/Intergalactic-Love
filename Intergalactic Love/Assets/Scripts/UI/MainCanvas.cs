using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public CraftingSystemUI craftingSystem;
    public DialogueSystem dialogueSystem;

    public ConfirmationMessageUI confirmationMessage;

    public ItemPopupParent itemPopupParent;

    public NewRecipePopup newRecipePopup;

    public GameObject interactTooltip;

    public UITips uiTips;

    public CanvasGroup blackFondue;

    private bool isInventoryDisplayed = false;
    public bool IsInventoryOpened()
    { return isInventoryDisplayed; }

    private bool isCraftingDisplayed = false;
    public bool IsCraftingDisplayed()
    { return isCraftingDisplayed; }

    public void Initialize()
    {
        craftingSystem.Initialize();

        inventoryUI.gameObject.SetActive(false);
        craftingSystem.gameObject.SetActive(false);
        dialogueSystem.Initialize();

        confirmationMessage.Initiliaze();

        uiTips.Initialize();

        itemPopupParent.Initialize();
    }

    public void Update()
    {
        if (GameManager.gm.canPlayerDoAnything && !dialogueSystem.IsExecutingDialogue())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Interactible.targetedItem != null)
                {
                    Interactible.targetedItem.Interact(GameManager.gm.player);
                }
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                isInventoryDisplayed = !isInventoryDisplayed;
                inventoryUI.gameObject.SetActive(isInventoryDisplayed);

                if (isInventoryDisplayed)
                {
                    inventoryUI.Initialize();
                    GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.openInventory);
                }
                else
                {
                    GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.closeInventory);
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
                    GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.openCraftingTable);
                }
                else
                {
                    GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.closeCraftingTable);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCraftingDisplayed)
            {
                isCraftingDisplayed = false;
                craftingSystem.gameObject.SetActive(isCraftingDisplayed);
                GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.closeInventory);
            }

            if (isInventoryDisplayed)
            {
                isInventoryDisplayed = false;
                inventoryUI.gameObject.SetActive(isInventoryDisplayed);
                GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.closeCraftingTable);
            }
        }
    }

    public void ShowInteractTooltip(bool enable)
    {
        interactTooltip.SetActive(enable);
    }

}
