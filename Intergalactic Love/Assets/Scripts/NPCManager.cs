using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private Texture playersPortrait;
    [SerializeField] private CharacterData[] characters;

    private Dictionary<string, Texture> portraits;

    public Texture GetPortrait(string name)
    {
        if (name.Equals("player"))
            return playersPortrait;
        return portraits[name];
    }

    private void Start()
    {
        portraits = new Dictionary<string, Texture>();
        foreach (CharacterData cd in characters)
        {
            portraits.Add(cd.name, cd.portrait);
        }
    }

    [System.Serializable]
    public class CharacterData
    {
        public Texture portrait;
        public string name;
    }
}
