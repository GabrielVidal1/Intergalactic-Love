using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : Interactible
{
    [SerializeField] private QuestEvent[] events;
    [SerializeField] private bool onlyOneTrigger;

    private bool hasBeenTriggered = false;

    private AudioSource audio;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        audio = GetComponent<AudioSource>();
    }

    public override void Interact(Player player)
    {
        if(audio != null)
            audio.Play();

        hasBeenTriggered = true;
        StartCoroutine(ExecuteEvents());
        base.SetObjectAsTarget(false);
    }

    protected override void SetObjectAsTarget(bool enable)
    {
        if (onlyOneTrigger && !hasBeenTriggered)
            base.SetObjectAsTarget(enable);
    }

    IEnumerator ExecuteEvents()
    {
        foreach (QuestEvent e in events)
        {
            yield return StartCoroutine(e.Invoke());
        }
    }
}
