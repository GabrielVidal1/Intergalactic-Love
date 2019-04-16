using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform spaceship;

    [Range(0.00001f, 1f)]
    [SerializeField] private float smoothCoef;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, spaceship.position, smoothCoef);
    }
}
