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
    public Player player;

    public DroppedItem droppedItemPrefab;

    public void Start()
    {
        itemManager = GetComponent<ItemManager>();
        mainCanvas = GameObject.FindObjectOfType<MainCanvas>();
        player = GameObject.FindObjectOfType<Player>();
    }

    public bool CanPlayerMove()
    {
        return !mainCanvas.IsInventoryOpened();
    }
}
