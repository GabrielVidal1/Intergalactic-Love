using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabotible : Interactible
{
    public GameObject vaisseau;


    public override void Interact(Player player)
    {
        Destroy(vaisseau);
    }
}
