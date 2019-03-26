using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRigidbody : MonoBehaviour
{
    public bool isAffectedByGravity;
    public float gravity;

    [SerializeField]
    private Attractor attractor;
    private Rigidbody rb;
    private Vector3 acceleration;
    private Vector3 up;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        up = Vector3.up;

        if (attractor != null)
            up = attractor.Attract(transform);

        acceleration = -gravity * up;

        rb.velocity += acceleration;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Attractor"))
        {
            attractor = collision.collider.GetComponent<Attractor>();
        }
    }
}
