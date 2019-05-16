using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthOn : MonoBehaviour
{
    private bool notStealth = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!notStealth)
        {
            if (other.tag.Equals("Player"))
            {
                Animator anim = other.GetComponent<Animator>();
                anim.SetLayerWeight(2, 1.0f);
                anim.SetLayerWeight(1, 0.0f);
                notStealth = true;
            }
        }
    }
}
