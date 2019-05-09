using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEvent : MonoBehaviour
{
    [SerializeField] private QuestEvent[] events;

    void Start()
    {
        StartCoroutine(ExecuteEvents());
    }

    IEnumerator ExecuteEvents()
    {
        foreach (QuestEvent e in events)
        {
            yield return StartCoroutine(e.Invoke());
        }
    }
}
