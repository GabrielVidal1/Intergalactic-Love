using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Material newQuest;
    public Material updateQuest;
    public Material updateQuestNone;
    public Material dialogueBubble;

    public QuestIcons questIconPrefab;

    private Dictionary<Quest.ValidatorType, List<NPC>> pendingQuestNPCs;
    public List<ItemCollectionEvent> pendingEvents;

    private void Awake()
    {
        pendingEvents = new List<ItemCollectionEvent>();
        pendingQuestNPCs = new Dictionary<Quest.ValidatorType, List<NPC>>();
    }

    public void UpdateQuestNPC(Quest.ValidatorType validatorType)
    {
        if (pendingQuestNPCs.ContainsKey(validatorType))
        {
            foreach (NPC npc in pendingQuestNPCs[validatorType])
            {
                npc.UpdateQuestStatus();
            }
        }

        foreach (ItemCollectionEvent e in pendingEvents)
        {
            e.UpdateStatus();
        }
    }

    public void AddNPCToPending(Quest.ValidatorType validatorType, NPC npc)
    {
        if (!pendingQuestNPCs.ContainsKey(validatorType))
        {
            pendingQuestNPCs[validatorType] = new List<NPC>();
        }
        pendingQuestNPCs[validatorType].Add(npc);
    }

    public void RemoveNPCToPending(Quest.ValidatorType validatorType, NPC npc)
    {
        if (!pendingQuestNPCs.ContainsKey(validatorType)) return;
        pendingQuestNPCs[validatorType].Remove(npc);
    }
}
