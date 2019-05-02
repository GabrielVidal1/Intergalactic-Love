using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactible
{
    public Dialogue[] dialogues;

    [SerializeField] private Transform questIconParent;

    private Quest.QuestPart currentQuestPart;

    private Quest quest;
    private int index;

    private void Start()
    {
        QuestIcons questIcons = Instantiate(GameManager.gm.questManager.questIconPrefab);
        questIcons.transform.SetParent(questIconParent);
        questIcons.transform.localPosition = Vector3.zero;
        questIcons.transform.localRotation = Quaternion.identity;

        questIcons.Disable();
    }

    public override void Interact(Player player)
    {
        if (currentQuestPart == null)
        {
            Dialogue selectedDialogue = dialogues[Random.Range(0, dialogues.Length - 1)];
            GameManager.gm.mainCanvas.dialogueSystem.ExecuteDialogue(selectedDialogue);
        }
        else
        {
            if (GetQuestValidation())
            {
                GameManager.gm.mainCanvas.dialogueSystem.ExecuteDialogue(currentQuestPart.dialogue);
            }
            else
            {
                Dialogue selectedDialogue = currentQuestPart.defaultDialogues[Random.Range(0, currentQuestPart.defaultDialogues.Length - 1)];
                GameManager.gm.mainCanvas.dialogueSystem.ExecuteDialogue(selectedDialogue);
            }
        }
    }

    public void SetQuestPart(Quest quest, int index)
    {
        if (index >= quest.parts.Length) return;

        currentQuestPart = quest.parts[index];

        this.quest = quest;
        this.index = index;
    }

    public bool GetQuestValidation()
    {
        return currentQuestPart.validator == null ||
            currentQuestPart.validator.HasBeenValidated();
    }

    public void EndDialogue()
    {
        if (GetQuestValidation())
        {
            quest.ExecuteQuest(index + 1);
            currentQuestPart = null;
        }
    }
}
