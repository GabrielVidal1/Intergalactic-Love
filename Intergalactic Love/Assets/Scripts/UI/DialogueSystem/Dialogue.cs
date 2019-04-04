using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "", order = 1)]
public class Dialogue : ScriptableObject
{
    public Protagonist[] protagonists;
    public DialogueLine[] lines;

    [System.Serializable]
    public class Protagonist
    {
        public string name;
        public Texture portrait;
    }

    [System.Serializable]
    public class DialogueLine
    {
        public int protagonistIndex;
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

