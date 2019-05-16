using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    
    public Transform respawnPoint;
    private Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.GetComponent<Transform>();
            StartCoroutine(BlackFondue());
        }
    }

    IEnumerator BlackFondue()
    {
        for (float i = 0f; i < 1f; i += 0.1f)
        {
            GameManager.gm.mainCanvas.blackFondue.alpha = i;
            yield return 0;
        }
        GameManager.gm.mainCanvas.blackFondue.alpha = 1f;

        playerTransform.position = respawnPoint.position;

        GameManager.gm.mainCanvas.blackFondue.alpha = 0.0f;

    }
}
