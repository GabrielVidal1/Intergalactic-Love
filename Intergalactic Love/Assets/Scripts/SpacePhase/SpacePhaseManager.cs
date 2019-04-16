using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePhaseManager : MonoBehaviour
{
    void Start()
    {
        GameManager.gm.player.DisablePlayer();
    }


    public Camera mainCamera;

}
