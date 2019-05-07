using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(BlackFondue());
        }
    }

    IEnumerator BlackFondue()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for (float i = 0f; i < 1f; i += 0.1f)
        {
            GameManager.gm.mainCanvas.blackFondue.alpha = i;
            yield return 0;
        }
        GameManager.gm.mainCanvas.blackFondue.alpha = 1f;


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
