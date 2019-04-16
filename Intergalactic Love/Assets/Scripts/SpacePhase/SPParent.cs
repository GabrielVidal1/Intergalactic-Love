using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPParent : MonoBehaviour
{
    private SpaceshipSaveLoad spaceshipSaveLoad;

    void Start()
    {
        spaceshipSaveLoad = GetComponent<SpaceshipSaveLoad>();
        spaceshipSaveLoad.LoadSpaceship();
    }
}
