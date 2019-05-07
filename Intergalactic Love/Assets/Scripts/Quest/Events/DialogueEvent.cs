using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : QuestEvent
{
    [SerializeField] private Dialogue dialogue;

    private void Start()
    {
        shouldAnimate = false;
    }

    protected override IEnumerator Execute()
    {
        print("Executing dialogue...");
        yield return StartCoroutine(GameManager.gm.mainCanvas.dialogueSystem.ExecuteDialogue(dialogue));
        print("Done Executing dialogue!");
    }
}
