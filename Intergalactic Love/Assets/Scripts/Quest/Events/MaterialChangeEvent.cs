using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChangeEvent : QuestEvent
{
    public MeshRenderer target;
    public Material newMaterial;

    public override void Invoke()
    {
        target.material = newMaterial;
    }
}
