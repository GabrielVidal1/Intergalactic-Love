using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Texture texture;

    [HideInInspector] public int index;
}
