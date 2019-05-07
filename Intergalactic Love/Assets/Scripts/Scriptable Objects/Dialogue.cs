using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "", order = 1)]
public class Dialogue : ScriptableObject
{
    [SerializeField] private string protagonist1;
    [SerializeField] private string protagonist2;
    public DialogueLine[] lines;

    public Protagonist GetProtagonist1()
    { return new Protagonist(protagonist1); }

    public Protagonist GetProtagonist2()
    { return new Protagonist(protagonist2); }

    public class Protagonist
    {
        public string name;
        public Texture portrait;
        public AudioClip voice;

        public Protagonist(string name)
        {
            portrait = GameManager.gm.NPCManager.GetPortrait(name);
            voice = GameManager.gm.NPCManager.GetVoice(name);
            this.name = name;
        }

    }

    [System.Serializable]
    public class DialogueLine
    {
        public ProtagonistPos protagonistPos;

        public enum ProtagonistPos
        {
            First,
            Second
        }

        public ProtagonistPosition position;
        public string line;
        public DialogueLineStyle style;

        public enum DialogueLineStyle
        {
            Normal,
            Italic,
            Bold
        }

        public enum ProtagonistPosition
        {
            Left,
            Right
        }
    }
}

