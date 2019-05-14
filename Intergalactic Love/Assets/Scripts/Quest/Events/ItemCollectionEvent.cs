using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectionEvent : MonoBehaviour
{
    [SerializeField] private ItemAndQuantity[] requiredItems;
    [SerializeField] private QuestEvent[] events;

    private PlayerInventory playerInventory;

    private bool triggered;

    private void Start()
    {
        playerInventory = GameManager.gm.player.playerInventory;
        GameManager.gm.questManager.pendingEvents.Add(this);

        triggered = false;
    }

    public void UpdateStatus()
    {
        if (!triggered)
            triggered = true;
        else
            return;


        foreach (ItemAndQuantity item in requiredItems)
        {
            if (playerInventory.inventory.ContainsKey(item.item))
            {
                if (playerInventory.inventory[item.item] < item.quantity)
                    return;
            }
            else return;
        }

        StartCoroutine(TriggerEvents());
    }

    IEnumerator TriggerEvents()
    {
        foreach (QuestEvent e in events)
        {
            yield return StartCoroutine(e.Invoke());
        }
        GameManager.gm.questManager.pendingEvents.Remove(this);
    }

    [System.Serializable]
    public class ItemAndQuantity
    {
        public ItemData item;
        public int quantity;
    }
}
