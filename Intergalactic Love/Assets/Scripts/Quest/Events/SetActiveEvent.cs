using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveEvent : QuestEvent
{
    public GameObject[] objects;
    public bool active;

    protected override IEnumerator Execute()
    {
        foreach (GameObject item in objects)
        {
            item.SetActive(active);
        }
        yield break;
    }
}
