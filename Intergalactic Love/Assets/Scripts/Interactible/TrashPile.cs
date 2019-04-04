using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPile : Interactible
{
    public RandomItem[] items;

    [SerializeField] private float projForce = 5f;

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
        Vector3 localUp = transform.up;
        Vector3 u = Vector3.Cross(localUp, new Vector3(65f, 0.54f, -8f));
        Vector3 v = Vector3.Cross(localUp, u);


        foreach (RandomItem item in items)
        {
            if (Random.value < item.chance)
            {
                DroppedItem r = Instantiate(GameManager.gm.droppedItemPrefab, transform.position, Quaternion.identity);

                r.associatedItem = item.item;
                r.GetComponent<CustomRigidbody>().SetAttractor(associatedAttractor);

                float angle = Random.value * 2 * Mathf.PI;

                Vector3 dir = (u * Mathf.Cos(angle) + v * Mathf.Sin(angle)).normalized;
                dir = (dir + localUp).normalized * projForce;

                r.GetComponent<Rigidbody>().velocity = dir;
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