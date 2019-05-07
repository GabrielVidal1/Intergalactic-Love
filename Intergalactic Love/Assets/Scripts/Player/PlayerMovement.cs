using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float jumpHeight;

    [SerializeField] private float camRotSpeed = 3f;

    [SerializeField] private Transform camPivotTransform;

    [SerializeField] private float camDistance = 6f;

    //[SerializeField] private Transform camRot;

    private Rigidbody rb;
    private float jumpForce;

    [SerializeField]
    private Attractor mainAttractor;
    private Vector3 up;

    private bool canJump = true;

    private float camRotXAngle;
    private Camera mainCam;

    private LayerMask mask;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        up = Vector3.up;
        isDisabled = false;
        mainCam = GameManager.gm.player.mainCam;

        mask = ~LayerMask.GetMask("UI");
    }

    private bool isDisabled;

    void Update()
    {
        if (!isDisabled)
        {

            Vector3 localDir = Vector3.zero;

            if (GameManager.gm.CanPlayerMove())
            {
                #region Movement
                if (Input.GetKey(KeyCode.W))
                {
                    localDir += Vector3.forward;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    localDir -= Vector3.forward;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    localDir += Vector3.left;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    localDir -= Vector3.left;
                }

                if (canJump && Input.GetKeyDown(KeyCode.Space))
                {
                    GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.playerJump);

                    jumpForce = jumpHeight;
                    canJump = false;
                }

                #endregion

                #region Camera

                if (Input.GetMouseButtonDown(1))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }

                if (Input.GetMouseButton(1))
                {
                    float xRatio = Input.GetAxis("Mouse X");
                    float yRatio = Input.GetAxis("Mouse Y");

                    transform.rotation *= Quaternion.Euler(0, xRatio * camRotSpeed, 0);

                    camRotXAngle += -yRatio * camRotSpeed;
                    camRotXAngle = Mathf.Clamp(camRotXAngle, -50, 30);
                    camPivotTransform.localRotation = Quaternion.Euler(camRotXAngle, 0, 0);
                    //print(transform.up);
                }

                Ray ray = new Ray(camPivotTransform.position - mainCam.transform.forward, -mainCam.transform.forward);

                Debug.DrawRay(camPivotTransform.position - mainCam.transform.forward, -mainCam.transform.forward * camDistance, Color.red, 1f);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, camDistance, mask))
                {
                    mainCam.transform.position = hit.point - mainCam.transform.forward * 0.1f;
                }
                else
                {
                    mainCam.transform.localPosition = new Vector3(0, 0, -camDistance);
                }

                if (Input.GetMouseButtonUp(1))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                #endregion
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            jumpForce *= 0.9f;

            if (mainAttractor != null)
                up = mainAttractor.Attract(transform);

            rb.velocity = transform.TransformDirection(localDir).normalized * speed + -up * (gravity - jumpForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;

        if (collision.collider.CompareTag("Attractor"))
        {
            mainAttractor = collision.collider.GetComponent<Attractor>();
        }
    }

    public void DisablePlayer()
    {
        camPivotTransform.gameObject.SetActive(false);
        isDisabled = true;
    }
}
