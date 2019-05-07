using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(CustomRigidbody))]
public class Collectible : MonoBehaviour
{
    //used for when the player gets near by, he automatically collects the item
    //used for money and dropped Items

    [SerializeField] private Collider itemCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            bool hasBeenCollected = player.playerInventory.Collect(this);
            if (hasBeenCollected)
            {
                StartCoroutine(CollectCoroutine(player));
            }
        }
    }

    IEnumerator CollectCoroutine(Player player)
    {
        itemCollider.enabled = false;
        GetComponent<CustomRigidbody>().enabled = false;

        while ((transform.position - player.transform.position).sqrMagnitude > 0.05f)
        {
            yield return 0;
            transform.localScale *= 0.9f;
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.2f);
        }
        Destroy(gameObject);
    }
}
