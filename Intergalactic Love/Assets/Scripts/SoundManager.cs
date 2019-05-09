using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup mixerGroup;

    public AudioClip openInventory;
    public AudioClip closeInventory;

    public AudioClip openCraftingTable;
    public AudioClip closeCraftingTable;

    public AudioClip pickupItemFromGround;
    public AudioClip putItemInInventory;

    public AudioClip pickupItemFromInventory;
    public AudioClip dropItemFromInventory;

    public AudioClip clickOnCraft;
    public AudioClip successfulCraft;
    public AudioClip failedCraft;

    public AudioClip cantClickHere;
    public AudioClip clickOnRecipe;

    public AudioClip playerJump;

    public AudioClip newQuestSound;

    public AudioClip defaultFootstep;

    public AudioClip newRecipe;

    public AudioClip tipOff;
    public AudioClip tipOn;


    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip == null) return;

        GameObject g = new GameObject("sound");
        AudioSource s = g.AddComponent<AudioSource>();

        s.clip = audioClip;
        s.outputAudioMixerGroup = mixerGroup;

        s.Play();
        g.transform.SetParent(GameManager.gm.player.transform);
        g.transform.localPosition = Vector3.zero;

        Destroy(g, audioClip.length);
    }

}
