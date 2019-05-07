using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private Texture playersPortrait;
    [SerializeField] private AudioClip playersVoice;

    [SerializeField] private CharacterData[] characters;

    private Dictionary<string, Texture> portraits;
    private Dictionary<string, AudioClip> voices;

    public Texture GetPortrait(string name)
    {
        if (name.Equals("player"))
            return playersPortrait;
        return portraits[name];
    }

    public AudioClip GetVoice(string name)
    {
        if (name.Equals("player"))
            return playersVoice;
        return voices[name];
    }

    private void Start()
    {
        portraits = new Dictionary<string, Texture>();
        voices = new Dictionary<string, AudioClip>();

        foreach (CharacterData cd in characters)
        {
            portraits.Add(cd.name, cd.portrait);
            voices.Add(cd.name, cd.voice);
        }
    }

    [System.Serializable]
    public class CharacterData
    {
        public Texture portrait;
        public string name;
        public AudioClip voice;
    }
}
