using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class CraftingStation : Interactible
{
    [Header("Infos")]

    public string stationDescription;
    public Texture stationBackground;

    public ItemQuantity[] input;
    public ItemQuantity[] output;

    [SerializeField] private AudioClip actionSound;

    [SerializeField] private Transform IconParent;

    private Animator animator;
    private AudioSource audioSource;
    private PlayerInventory playerInv;

    private bool isUsed = false;

    private CraftingStationIcons craftingStationIcons;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = actionSound;

        playerInv = GameManager.gm.player.playerInventory;

        craftingStationIcons = Instantiate(GameManager.gm.recipeManager.craftingStationIconsPrefab, IconParent);
        craftingStationIcons.transform.localPosition = Vector3.zero;
        craftingStationIcons.transform.localRotation = Quaternion.identity;

        craftingStationIcons.SetState(State.None);
    }

    public override void Interact(Player player)
    {
        if (!isUsed && HasInputItems())
        {
            isUsed = true;
            animator.SetTrigger("Execute");
            audioSource.Play();

            craftingStationIcons.SetState(State.Using);
        }
        else
        {
            GameManager.gm.mainCanvas.craftingStationUITip.ShowTipForStation(this);
        }
    }

    private bool HasInputItems()
    {
        foreach (ItemQuantity item in input)
        {
            if (playerInv.inventory.ContainsKey(item.item))
                return playerInv.inventory[item.item] >= item.amount;
            else
                return false;
        }
        return true;
    }

    public void RewardPlayer()
    {
        foreach (ItemQuantity item in output)
        {
            GameManager.gm.player.playerInventory.AddItemToInventory(item.item, item.amount);
        }
        isUsed = false;
        craftingStationIcons.SetState(State.None);

        audioSource.Stop();

        SetObjectAsTarget(true);
    }

    protected override void SetObjectAsTarget(bool enable)
    {
        if (enable)
        {
            if (HasInputItems())
                craftingStationIcons.SetState(State.CanUse);
            else
                craftingStationIcons.SetState(State.CantUse);
        }
        else
            craftingStationIcons.SetState(State.None);


        base.SetObjectAsTarget(enable);
    }

    [System.Serializable]
    public class ItemQuantity
    {
        public int amount;
        public ItemData item;
    }

    public enum State
    {
        Using,
        CanUse,
        CantUse,
        None
    }
}
