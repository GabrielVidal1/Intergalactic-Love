using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(SpaceshipSaveLoad))]
public class SpaceshipStation : Interactible
{

    private SpaceshipSaveLoad sSaveLoad;

    public override void Interact(Player player)
    {
        base.Interact(player);

        GameManager.gm.mainCanvas.confirmationMessage.TriggerMessage(
            "Do you want to edit your ship?",
            "Yes",
            TriggerShipEditing,
            null);
    }

    private void TriggerShipEditing()
    {
        SaveLoad.SaveGame();
        SceneManager.LoadScene("SpaceshipEditor");
    }


    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        sSaveLoad = GetComponent<SpaceshipSaveLoad>();
        sSaveLoad.LoadSpaceship();
    }


}
