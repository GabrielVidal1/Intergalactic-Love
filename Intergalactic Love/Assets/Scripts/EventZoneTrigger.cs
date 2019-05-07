using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZoneTrigger : MonoBehaviour
{
    [SerializeField] private QuestEvent[] events;

    [SerializeField] private bool onlyOneTrigger;

    private bool hasBeenTriggered = false;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (onlyOneTrigger && !hasBeenTriggered || !onlyOneTrigger)
            {
                hasBeenTriggered = true;
                StartCoroutine(ExecuteEvents());
            }
        }
    }
    
    IEnumerator ExecuteEvents()
    {
        foreach (QuestEvent e in events)
        {
            yield return StartCoroutine(e.Invoke());
        }
    }

}
