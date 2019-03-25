using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;

    public float mass;
    public Texture texture;
}
