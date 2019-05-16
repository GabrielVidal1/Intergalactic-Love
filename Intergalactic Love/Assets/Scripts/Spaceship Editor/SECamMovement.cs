using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECamMovement : MonoBehaviour
{
    // Update is called once per frame

    private bool isDragging;

    [SerializeField] private Transform camRotPivot;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float camRotSpeed;
    [SerializeField] private float zoomSpeed;

    private float camRotXAngle = 45f;
    private float camZ = 5f;

    private void Start()
    {
        camRotPivot.localRotation = Quaternion.Euler(camRotXAngle, 0, 0);
    }

    void Update()
    {
        if (!GameManager.gm.canPlayerDoAnything) return;

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
            camRotXAngle = Mathf.Clamp(camRotXAngle, 1, 89);
            camRotPivot.localRotation = Quaternion.Euler(camRotXAngle, 0, 0);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        camZ = Mathf.Clamp(camZ - Input.mouseScrollDelta.y * zoomSpeed, 2, 10);
        mainCam.transform.localPosition = new Vector3(0, 0, -camZ);
    }
}
