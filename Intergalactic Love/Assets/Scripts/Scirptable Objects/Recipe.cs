using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "", order = 1)]
public class Recipe : ScriptableObject
{
    public ItemData result;
    public int amount;

    public ItemData[] ingredients;

    [HideInInspector] public int index;
}
