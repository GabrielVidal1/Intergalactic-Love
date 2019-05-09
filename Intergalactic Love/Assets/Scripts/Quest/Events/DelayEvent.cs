using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEvent : QuestEvent
{
    [SerializeField] private QuestEvent[] events;

    [SerializeField] private float delay;

    protected override IEnumerator Execute()
    {
        StartCoroutine(ExecuteEvents());
        yield break;
    }

    IEnumerator ExecuteEvents()
    {
        yield return new WaitForSecondsRealtime(delay);

        foreach (QuestEvent e in events)
        {
            yield return StartCoroutine(e.Invoke());
        }
    }

}
