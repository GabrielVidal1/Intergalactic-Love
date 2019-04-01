﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public PlayerMovement playerMovement;

    public void Initialize()
    {
        playerInventory = GetComponent<PlayerInventory>();
        playerInventory.Initialize(this);

        playerMovement= GetComponent<PlayerMovement>();
    }


}
