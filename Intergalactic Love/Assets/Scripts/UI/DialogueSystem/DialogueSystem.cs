using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Text;

public class DialogueSystem : MonoBehaviour
{
    private static NPC npc;


    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private RawImage portraitLeft;
    [SerializeField] private TextMeshProUGUI portraitLeftName;
    [SerializeField] private RawImage portraitRight;
    [SerializeField] private TextMeshProUGUI portraitRightName;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private float speed;

    private bool isExecutingDialogue;

    public bool IsExecutingDialogue()
    { return isExecutingDialogue; }

    public void Initialize()
    {
        dialoguePanel.SetActive(false);
        isExecutingDialogue = false;
    }

    public void ExecuteDialogueFromNPC(NPC npc, Dialogue dialogue)
    {
        DialogueSystem.npc = npc;
        ExecuteDialogue(dialogue);
    }

    public void ExecuteDialogue(Dialogue dialogue)
    {
        if (!isExecutingDialogue)
        {
            StartCoroutine(ExecuteDialogueCoroutine(dialogue));
            isExecutingDialogue = true;
        }
    }

    IEnumerator ExecuteDialogueCoroutine(Dialogue dialogue)
    {
        dialoguePanel.SetActive(true);

        foreach (Dialogue.DialogueLine line in dialogue.lines)
        {
            SetUp(line, dialogue);

            StringBuilder sb = new StringBuilder();
            text.text = "";

            for (int i = 0; i < line.line.Length; i++)
            {
                sb.Append(line.line[i]);
                text.text = sb.ToString();

                switch (line.style)
                {
                    case Dialogue.DialogueLine.DialogueLineStyle.Italic:
                        text.fontStyle = FontStyles.Italic;
                        break;
                    case Dialogue.DialogueLine.DialogueLineStyle.Bold:
                        text.fontStyle = FontStyles.Bold;
                        break;
                    default:
                        text.fontStyle = FontStyles.Normal;
                        break;
                }

                yield return new WaitForSecondsRealtime(Time.deltaTime / 2);
            }

            while (!Input.GetKeyDown(KeyCode.Space))
                yield return 0;
        }

        dialoguePanel.SetActive(false);
        isExecutingDialogue = false;

        if (npc != null)
        {
            npc.EndDialogue();
            npc = null;
        }
    }

    private void SetUp(Dialogue.DialogueLine line, Dialogue dialogue)
    {
        Dialogue.Protagonist protagonist = 
            line.protagonistPos == Dialogue.DialogueLine.ProtagonistPos.First ? 
            dialogue.protagonist1 : dialogue.protagonist2;

        switch (line.position)
        {
            case Dialogue.DialogueLine.ProtagonistPosition.Left:
                portraitLeft.gameObject.SetActive(true);
                portraitLeft.texture = protagonist.portrait;
                portraitLeftName.text = protagonist.name;

                portraitRight.gameObject.SetActive(false);
                portraitRightName.text = "";
                break;
            case Dialogue.DialogueLine.ProtagonistPosition.Right:
                portraitRight.gameObject.SetActive(true);
                portraitRight.texture = protagonist.portrait;
                portraitRightName.text = protagonist.name;

                portraitLeft.gameObject.SetActive(false);
                portraitLeftName.text = "";
                break;
        }
    }
}
