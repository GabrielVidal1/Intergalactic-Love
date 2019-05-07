using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] public GameObject flag;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckpointManager.SetCheckpoint(this);
    }

    public void SetFlag(bool enable)
    {
        flag.SetActive(enable);
    }
}
