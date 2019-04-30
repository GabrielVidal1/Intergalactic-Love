using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager gm;
    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(gameObject);
    }
    #endregion

    public ItemManager itemManager;
    public MainCanvas mainCanvas;
    public RecipeManager recipeManager;
    public MainCanvasSE mainCanvasSE;
    public Player player;

    public DroppedItem droppedItemPrefab;

    public void Start()
    {
        //ALWAYS PRESENT
        recipeManager = GetComponent<RecipeManager>();
        recipeManager.Initialize();
        itemManager = GetComponent<ItemManager>();

        //NOT ALWAYS PRESENT
        mainCanvas = GameObject.FindObjectOfType<MainCanvas>();
        mainCanvasSE = GameObject.FindObjectOfType<MainCanvasSE>();
        player = GameObject.FindObjectOfType<Player>();
        if (player != null)
            player.Initialize();
        if (mainCanvas != null)
            mainCanvas.Initialize();
       

        SaveLoad.LoadGame();
    }

    public bool CanPlayerMove()
    {
        return 
            !mainCanvas.IsInventoryOpened() &&
            !mainCanvas.IsCraftingDisplayed() &&
            !mainCanvas.dialogueSystem.IsExecutingDialogue();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            SaveLoad.SaveGame();
            Debug.Log("Save Game Complete!");
        }
    }
}
