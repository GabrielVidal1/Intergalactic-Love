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
        {
            gm = this;
            //Initialize();
        }
        else if (gm != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        InitAll();
    }
    #endregion

    public ItemManager itemManager;
    public MainCanvas mainCanvas;
    public RecipeManager recipeManager;
    public MainCanvasSE mainCanvasSE;
    public NPCManager NPCManager;
    public SoundManager soundManager;

    public SpacePhaseManager spacePhaseManager;

    public Player player;

    public DroppedItem droppedItemPrefab;

    public QuestManager questManager;

    public bool canPlayerDoAnything;

    public Itinerary currentItinerary;

    public bool shouldDisplayTips = true;

    private void InitAll()
    {
        canPlayerDoAnything = true;

        print("GameManager called InitAll()");
        //ALWAYS PRESENT
        recipeManager = GetComponent<RecipeManager>();
        recipeManager.Initialize();
        itemManager = GetComponent<ItemManager>();
        itemManager.Initialize();
        questManager = GetComponent<QuestManager>();
        NPCManager = GetComponent<NPCManager>();
        soundManager = GetComponent<SoundManager>();

        //NOT ALWAYS PRESENT
        mainCanvas = GameObject.FindObjectOfType<MainCanvas>();
        mainCanvasSE = GameObject.FindObjectOfType<MainCanvasSE>();
        player = GameObject.FindObjectOfType<Player>();
        if (player != null)
            player.Initialize();
        if (mainCanvas != null)
            mainCanvas.Initialize();
       

        SaveLoad.LoadGame();

        canPlayerDoAnything = true;
    }

    public bool CanPlayerMove()
    {
        if (mainCanvas == null) return false;

        return 
            !mainCanvas.IsInventoryOpened() &&
            !mainCanvas.IsCraftingDisplayed() &&
            !mainCanvas.dialogueSystem.IsExecutingDialogue() &&
            canPlayerDoAnything;
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
