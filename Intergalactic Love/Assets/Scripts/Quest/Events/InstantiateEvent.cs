using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEvent : QuestEvent
{
    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private Transform targetParent;

    [SerializeField] private GameObject tempInstantiate;
    [SerializeField] private float destroyTempDelay;

    protected override IEnumerator Execute()
    {
        GameObject a = Instantiate(objectToInstantiate, targetParent);
        a.transform.localPosition = Vector3.zero;
        a.transform.localRotation = Quaternion.identity;

        if (tempInstantiate != null)
        {
            a = Instantiate(tempInstantiate, targetParent);
            a.transform.localPosition = Vector3.zero;
            a.transform.localRotation = Quaternion.identity;

            Destroy(a, destroyTempDelay);
        }
        yield break;
    }
}
