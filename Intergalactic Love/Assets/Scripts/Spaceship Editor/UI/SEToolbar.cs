using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEToolbar : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    private bool menuState = false;

    public void Initialize()
    {
        menu.SetActive(false);
    }

    public void ToggleMenu()
    {
        menuState = !menuState;
        menu.SetActive(menuState);
    }

    public void Save()
    {
        GameManager.gm.mainCanvasSE.spaceshipSaveLoad.SaveSpaceship();
    }

    public void GoBack()
    {

    }
}
