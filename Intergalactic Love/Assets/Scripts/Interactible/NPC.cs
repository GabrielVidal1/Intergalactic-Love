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
        StartCoroutine(Execute());
    }

    IEnumerator Execute()
    {
        if (currentQuestPart == null)
        {
            if (defaultDialogues.Length > 0)
            {
                Dialogue selectedDialogue = defaultDialogues[Random.Range(0, defaultDialogues.Length)];
                yield return StartCoroutine(GameManager.gm.mainCanvas.dialogueSystem.ExecuteDialogue(selectedDialogue));
            }
            else
            {
                throw new UnityException("The npc " + name + " has no default dialogues.");
            }
        }
        else
        {
            if (CanQuestBeValidated())
            {
                yield return StartCoroutine(GameManager.gm.mainCanvas.dialogueSystem.ExecuteDialogue(currentQuestPart.dialogue));

                if (currentQuestPart.validator != null)
                {
                    currentQuestPart.validator.ValidatePart();
                    GameManager.gm.questManager.RemoveNPCToPending(currentQuestPart.validator.GetValidatorType(), this);
                }

                currentQuestPart = null;
                UpdateQuestStatus();

                quest.ExecuteQuest(index + 1);
            }
            else
            {
                if (currentQuestPart.defaultDialogues.Length > 0)
                {
                    Dialogue selectedDialogue = currentQuestPart.defaultDialogues[Random.Range(0, currentQuestPart.defaultDialogues.Length)];
                    yield return StartCoroutine(GameManager.gm.mainCanvas.dialogueSystem.ExecuteDialogue(selectedDialogue));
                }
            }
        }
    }

    protected override void SetObjectAsTarget(bool enable)
    {
        if (!hasBeenInitialized)
            Initialize();

        base.SetObjectAsTarget(enable);

        if (currentQuestPart == null)
        {
            if (enable)
                questIcons.SetBubble(Quest.QuestType.DialogueBubble);
            else
                questIcons.Disable();
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
        print("UpdateQuestStatus()   " + index);
        if (currentQuestPart == null)
        {
            questIcons.SetBubble(Quest.QuestType.DialogueBubble);
            return;
        }

        if (CanQuestBeValidated())
        {

            print("CanQuestBeValidated()   " + index);

            if (index == 0)
                questIcons.SetBubble(Quest.QuestType.NewQuest);
            else
                questIcons.SetBubble(Quest.QuestType.UpdateQuest);
        }
        else
        {
            if (index > 0)
                questIcons.SetBubble(Quest.QuestType.UpdateQuestNone);
        }
    }

    public bool CanQuestBeValidated()
    {
        return currentQuestPart.validator == null ||
            currentQuestPart.validator.CanPartBeValidated();
    }

}
