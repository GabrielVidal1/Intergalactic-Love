using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene(2);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene(3);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(4);
        }
    }
}
