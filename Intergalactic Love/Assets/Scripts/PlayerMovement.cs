using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float jumpHeight;

    [SerializeField] private float camRotSpeed = 3f;

    //[SerializeField] private Transform camRot;

    private Rigidbody rb;
    private float jumpForce;

    //[SerializeField]
    private Attractor mainAttractor;
    private Vector3 up;

    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        up = Vector3.up;
    }

    void Update()
    {
        Vector3 localDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            localDir += Vector3.forward * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            localDir -= Vector3.forward * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            localDir += Vector3.left * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            localDir -= Vector3.left * speed;
        }
        
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            jumpForce = jumpHeight;
            canJump = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetMouseButton(1))
        {
            float xRatio = Input.GetAxis("Mouse X");
            transform.Rotate(transform.up, xRatio * camRotSpeed);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        jumpForce *= 0.9f;

        if (mainAttractor != null)
            up = mainAttractor.Attract(transform);

        rb.velocity = transform.TransformDirection(localDir) + - up * (gravity - jumpForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;

        if (collision.collider.CompareTag("Attractor"))
        {
            mainAttractor = collision.collider.GetComponent<Attractor>();
        }
    }
}
