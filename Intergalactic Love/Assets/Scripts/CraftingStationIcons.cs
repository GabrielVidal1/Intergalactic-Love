using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStationIcons : MonoBehaviour
{
    [SerializeField] private MeshRenderer quad1;
    [SerializeField] private MeshRenderer quad2;

    public void SetState(CraftingStation.State state)
    {
        quad1.gameObject.SetActive(true);
        quad2.gameObject.SetActive(true);

        switch (state)
        {
            case CraftingStation.State.Using:
                quad1.material = GameManager.gm.recipeManager.craftingStationIconIsUsed;
                quad2.material = GameManager.gm.recipeManager.craftingStationIconIsUsed;
                break;
            case CraftingStation.State.CanUse:
                quad1.material = GameManager.gm.recipeManager.craftingStationIconCanUse;
                quad2.material = GameManager.gm.recipeManager.craftingStationIconCanUse;
                break;
            case CraftingStation.State.CantUse:
                quad1.material = GameManager.gm.recipeManager.craftingStationIconCantUse;
                quad2.material = GameManager.gm.recipeManager.craftingStationIconCantUse;
                break;
            case CraftingStation.State.None:
                quad1.gameObject.SetActive(false);
                quad2.gameObject.SetActive(false);
                break;
        }
    }
}
