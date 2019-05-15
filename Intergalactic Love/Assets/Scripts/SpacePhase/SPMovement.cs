using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [Range(0.00001f, 1f)]
    [SerializeField] private float smoothSpeedCoef;

    [Range(0.00001f, 1f)]
    [SerializeField] private float rotSmoothCoef;


    [SerializeField] private float travelSpeed;

    private Quaternion targetRotation;
    private Vector3 targetPosition;

    public bool moving = false;

    private void Update()
    {
        if (!moving) return;

        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movement += Vector3.up;
        else if (Input.GetKey(KeyCode.S))
            movement += Vector3.down;

        if (Input.GetKey(KeyCode.A))
            movement += Vector3.left;
        else if (Input.GetKey(KeyCode.D))
            movement += Vector3.right;



        movement.Normalize();

        targetRotation = Quaternion.FromToRotation(Vector3.forward, (Vector3.forward + movement).normalized);

        targetPosition = transform.position + (movement * speed + travelSpeed * Vector3.forward ) * Time.deltaTime;

        Vector3 pos = Vector3.Lerp(transform.position, targetPosition, smoothSpeedCoef);

        pos.y = Mathf.Clamp(pos.y, -10, 10);

        transform.position = pos;

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSmoothCoef);
    }
}
