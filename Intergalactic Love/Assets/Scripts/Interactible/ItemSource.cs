using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSource : Interactible
{
    public RandomItem[] items;

    [SerializeField] private float projForce = 5f;

    [SerializeField] private bool isDestroyed;

    [SerializeField] private GameObject[] deactivatedObjects;

    [SerializeField] private bool regenerate;
    [SerializeField] private float regenerationDelay;

    private Attractor associatedAttractor;

    private bool empty = false;

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
        //SpawnItems();
        foreach (RandomItem item in items)
        {
            if (Random.value < item.chance)
            {
                GameManager.gm.player.playerInventory.AddItemToInventory(item.item, 1);
            }
        }

        SetObjectAsTarget(false);

        if (isDestroyed)
        {
            Destroy(gameObject);
            return;
        }

        empty = true;

        foreach (GameObject obj in deactivatedObjects)
        {
            obj.SetActive(false);
        }

        Invoke("Regenerate", regenerationDelay);
    }

    public void Regenerate()
    {
        empty = false;
        foreach (GameObject obj in deactivatedObjects)
        {
            obj.SetActive(true);
        }
    }

    protected override void SetObjectAsTarget(bool enable)
    {
        if (!empty)
            base.SetObjectAsTarget(enable);
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