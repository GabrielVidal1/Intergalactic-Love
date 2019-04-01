using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public static Interactible targetedItem;

    public virtual void Interact(Player player)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetObjectAsTarget(true);
        }
    }

    protected void SetObjectAsTarget(bool enable)
    {
        //Debug.Log("Called");
        GameManager.gm.mainCanvas.ShowInteractTooltip(enable);
        targetedItem = enable ? this : null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetObjectAsTarget(false);
        }
    }
}
