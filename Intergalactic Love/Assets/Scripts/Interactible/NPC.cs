using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactible
{
    public Dialogue[] defaultDialogues;

    [SerializeField] private Transform questIconParent;

    private Quest.QuestPart currentQuestPart;

    private Quest quest;
    private int index;

    private QuestIcons questIcons;

    private bool hasBeenInitialized = false;

    public bool IsInitialized()
    { return hasBeenInitialized; }

    public void Initialize()
    {
        questIcons = Instantiate(GameManager.gm.questManager.questIconPrefab.gameObject).GetComponent<QuestIcons>();
        questIcons.transform.SetParent(questIconParent);
        questIcons.transform.localPosition = Vector3.zero;
        questIcons.transform.localRotation = Quaternion.identity;

        questIcons.Disable();

        hasBeenInitialized = true;
    }

    public override void Interact(Player player)
    {
        if (currentQuestPart == null)
        {
            Dialogue selectedDialogue = defaultDialogues[Random.Range(0, defaultDialogues.Length - 1)];
            GameManager.gm.mainCanvas.dialogueSystem.ExecuteDialogueFromNPC(this, selectedDialogue);
        }
        else
        {
            if (CanQuestBeValidated())
            {
                GameManager.gm.mainCanvas.dialogueSystem.ExecuteDialogueFromNPC(this, currentQuestPart.dialogue);
            }
            else
            {
                if (currentQuestPart.defaultDialogues.Length > 0)
                {
                    Dialogue selectedDialogue = currentQuestPart.defaultDialogues[Random.Range(0, currentQuestPart.defaultDialogues.Length - 1)];
                    GameManager.gm.mainCanvas.dialogueSystem.ExecuteDialogueFromNPC(this, selectedDialogue);
                }
            }
        }
    }

    public void SetQuestPart(Quest quest, int index)
    {
        if (index >= quest.parts.Length) return;

        currentQuestPart = quest.parts[index];
        if (currentQuestPart.validator != null)
            GameManager.gm.questManager.AddNPCToPending(currentQuestPart.validator.GetValidatorType(), this);

        this.quest = quest;
        this.index = index;

        UpdateQuestStatus();
    }

    public void UpdateQuestStatus()
    {
        if (currentQuestPart == null)
        {
            questIcons.Disable();
            return;
        }

        if (CanQuestBeValidated())
        {
            if (index == 0)
                questIcons.SetQuest(Quest.QuestType.NewQuest);
            else
                questIcons.SetQuest(Quest.QuestType.UpdateQuest);
        }
        else
        {
            if (index > 0)
                questIcons.SetQuest(Quest.QuestType.UpdateQuestNone);
        }
    }

    public bool CanQuestBeValidated()
    {
        return currentQuestPart.validator == null ||
            currentQuestPart.validator.CanPartBeValidated();
    }

    public void EndDialogue()
    {
        if (currentQuestPart != null)
        {
            if (CanQuestBeValidated())
            {
                if (currentQuestPart.validator != null)
                {
                    currentQuestPart.validator.ValidatePart();
                    GameManager.gm.questManager.RemoveNPCToPending(currentQuestPart.validator.GetValidatorType(), this);
                }

                currentQuestPart = null;
                UpdateQuestStatus();

                quest.ExecuteQuest(index + 1);
            }
        }
    }
}
