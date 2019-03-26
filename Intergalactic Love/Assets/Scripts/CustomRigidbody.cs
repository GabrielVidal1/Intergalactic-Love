using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomRigidbody : MonoBehaviour
{
    public bool isAffectedByGravity = true;
    public float gravity = 10;

    [SerializeField]
    private Attractor attractor;
    private Rigidbody rb;
    private Vector3 acceleration;
    private Vector3 up;

    public void SetAttractor(Attractor attractor)
    {
        this.attractor = attractor;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.useGravity = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attractor"))
        {
            attractor = other.GetComponent<Attractor>();
        }
    }
}
