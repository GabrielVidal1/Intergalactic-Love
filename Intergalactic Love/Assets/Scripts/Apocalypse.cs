using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apocalypse : MonoBehaviour
{
    [SerializeField] private GameObject[] effect;

    [SerializeField] private Planet planet;

    private int index;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject e = Instantiate(effect[index], GameManager.gm.player.transform.position - GameManager.gm.player.transform.up*0.9f, Quaternion.identity);
            Planet.Orient(e.transform, planet);
            Destroy(e, 10f);

            index = (index + 1) % effect.Length;
        }
    }
}
