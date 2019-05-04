using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private bool shouldStartImmediatly;

    private void Start()
    {
        if (shouldStartImmediatly)
        {
            ExecuteQuest(0);
        }
    }

    public QuestPart[] parts;

    public QuestReward[] rewards;


    [System.Serializable]
    public class QuestPart
    {
        public DialogueChange[] dialogueChanges;
        public QuestEvent[] events;

        public Dialogue[] defaultDialogues;
        public QuestValidator validator;
        public NPC npc;
        public Dialogue dialogue;

        [System.Serializable]
        public class DialogueChange
        {
            public NPC npc;
            public Dialogue[] newDefaultDialogues;
        }

        
    }

    public enum QuestType
    {
        NewQuest,
        UpdateQuestNone,
        UpdateQuest
    }

    public enum ValidatorType
    {
        ConsumeItems,
        HasItems,
        GatherItems,
        Interact
    }

    public void ExecuteQuest(int index)
    {
        if (index >= parts.Length)
        {
            foreach (QuestReward reward in rewards)
            {
                reward.RewardPlayer();
            }
            return;
        }

        if (!parts[index].npc.IsInitialized())
            parts[index].npc.Initialize();

        parts[index].npc.SetQuestPart(this, index);

        foreach (QuestPart.DialogueChange dc in parts[index].dialogueChanges)
        {
            dc.npc.defaultDialogues = dc.newDefaultDialogues;
        }

        foreach (QuestEvent e in parts[index].events)
        {
            e.Invoke();
        }
    }
}
