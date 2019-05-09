using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : Interactible
{
    [SerializeField] private QuestEvent[] events;
    [SerializeField] private bool onlyOneTrigger;

    private bool hasBeenTriggered = false;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public override void Interact(Player player)
    {
        hasBeenTriggered = true;
        StartCoroutine(ExecuteEvents());
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
