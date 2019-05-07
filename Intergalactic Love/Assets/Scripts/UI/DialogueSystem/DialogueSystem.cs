using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Text;

public class DialogueSystem : MonoBehaviour
{
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

    public IEnumerator ExecuteDialogue(Dialogue dialogue)
    {
        if (dialogue == null) yield break;

        if (!isExecutingDialogue)
        {
            isExecutingDialogue = true;
            yield return StartCoroutine(ExecuteDialogueCoroutine(dialogue));
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                while (!Input.GetKeyUp(KeyCode.Space))
                    yield return 0;
            }

            int i = 0;

            while (i < line.line.Length && !Input.GetKeyDown(KeyCode.Space))
            { 
                sb.Append(line.line[i]);
                text.text = sb.ToString();

                yield return new WaitForSecondsRealtime(Time.deltaTime / 2);

                i++;
            }

            text.text = line.line;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                while (!Input.GetKeyUp(KeyCode.Space))
                    yield return 0;
            }
            while (!Input.GetKeyDown(KeyCode.Space))
                yield return 0;
        }

        dialoguePanel.SetActive(false);
        isExecutingDialogue = false;
    }

    private void SetUp(Dialogue.DialogueLine line, Dialogue dialogue)
    {
        Dialogue.Protagonist protagonist = 
            line.protagonistPos == Dialogue.DialogueLine.ProtagonistPos.First ? 
            dialogue.GetProtagonist1() : dialogue.GetProtagonist2();

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
