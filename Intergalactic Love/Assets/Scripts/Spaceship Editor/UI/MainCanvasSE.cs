using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasSE : MonoBehaviour
{
    public PartList partList;
    public Camera mainCam;

    [SerializeField] public Material notValidMat;

    private void Start()
    {
        partList.Initialize();

        GameManager.gm.player.DisablePlayer();
    }
}
