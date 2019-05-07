using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChangeEvent : QuestEvent
{
    public MeshRenderer target;
    public Material newMaterial;

    protected override IEnumerator Execute()
    {
        target.material = newMaterial;
        yield break;
    }
}
