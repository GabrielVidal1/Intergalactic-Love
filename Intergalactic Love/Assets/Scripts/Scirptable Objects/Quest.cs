using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "", order = 1)]
public class Quest : ScriptableObject
{
    public QuestPart[] parts;

    [System.Serializable]
    public class QuestPart
    {
        public NPC npc;
        public Dialogue[] defaultDialogues;

        public QuestValidator validator;
        public Dialogue dialogue;
    }

    public enum QuestType
    {
        NewQuest,
        UpdateQuestNone,
        UpdateQuest
    }

    public void ExecuteQuest(int index)
    {
        if (index >= parts.Length) return;

        parts[index].npc.SetQuestPart(this, index);
    }
}
