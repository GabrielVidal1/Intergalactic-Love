using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactible : MonoBehaviour
{
    public static Interactible targetedItem;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public abstract void Interact(Player player);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetObjectAsTarget(true);
        }
    }

    protected virtual void SetObjectAsTarget(bool enable)
    {
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
