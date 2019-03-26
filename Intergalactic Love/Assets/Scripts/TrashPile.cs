using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPile : Interactible
{
    public RandomItem[] items;

    private Attractor associatedAttractor;

    private void Start()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 2);

        foreach (Collider collider in cols)
        {
            if (collider.CompareTag("Attractor"))
            {
                associatedAttractor = collider.GetComponent<Attractor>();
            }
        }

    }

    public override void Interact(Player player)
    {
        SpawnItems();

        SetObjectAsTarget(false);

        Destroy(gameObject);
    }

    public void SpawnItems()
    {
        foreach (RandomItem item in items)
        {
            if (Random.value < item.chance)
            {
                DroppedItem r = Instantiate(GameManager.gm.droppedItemPrefab, transform.position, Quaternion.identity);

                r.associatedItem = item.item;
                r.GetComponent<CustomRigidbody>().SetAttractor(associatedAttractor);
            }
        }
    }
}

[System.Serializable]
public class RandomItem
{
    public ItemData item;
    public float chance;
}