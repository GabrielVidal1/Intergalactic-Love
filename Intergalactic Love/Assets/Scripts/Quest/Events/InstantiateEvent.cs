using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEvent : QuestEvent
{
    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private Transform targetParent;

    protected override IEnumerator Execute()
    {
        GameObject a = Instantiate(objectToInstantiate, targetParent);
        a.transform.localPosition = Vector3.zero;
        a.transform.localRotation = Quaternion.identity;

        yield break;
    }
}
