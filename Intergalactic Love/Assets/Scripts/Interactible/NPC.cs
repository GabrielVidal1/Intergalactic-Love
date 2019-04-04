using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactible
{
    public Dialogue dialogue;

    public override void Interact(Player player)
    {
        GameManager.gm.mainCanvas.dialogueSystem.ExecuteDialogue(dialogue);
    }

    public void EndDialogue()
    {

    }
}
