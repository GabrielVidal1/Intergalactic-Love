using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Material newQuest;
    public Material updateQuest;
    public Material updateQuestNone;

    public QuestIcons questIconPrefab;

    public Dictionary<Quest.ValidatorType, List<NPC>> pendingQuestNPCs;

    private void Start()
    {
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
