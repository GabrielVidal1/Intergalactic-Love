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
    public QuestEvent[] endEvents;


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
        DialogueBubble,
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

    public void StartQuest()
    { ExecuteQuest(0); }

    public void ExecuteQuest(int index)
    {
        print("Aaweaweawe");

        if (index >= parts.Length)
        {
            StartCoroutine(ExecuteEndEvents());
            return;
        }

        if (!parts[index].npc.IsInitialized())
            parts[index].npc.Initialize();

        print("parts[index].npc.SetQuestPart");

        parts[index].npc.SetQuestPart(this, index);

        foreach (QuestPart.DialogueChange dc in parts[index].dialogueChanges)
        {
            dc.npc.defaultDialogues = dc.newDefaultDialogues;
        }

        StartCoroutine(ExecuteEvents(index));
    }

    IEnumerator ExecuteEvents(int index)
    {
        foreach (QuestEvent e in parts[index].events)
        {
            yield return StartCoroutine(e.Invoke());
        }
    }

    IEnumerator ExecuteEndEvents()
    {
        foreach (QuestEvent e in endEvents)
        {
            yield return StartCoroutine(e.Invoke());
        }
    }

}
