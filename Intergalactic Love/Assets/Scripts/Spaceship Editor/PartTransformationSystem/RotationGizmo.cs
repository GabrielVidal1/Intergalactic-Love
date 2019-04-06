using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationGizmo : MonoBehaviour
{
    public Vector3 direction;

    private SpaceshipPart part;

    public void Initialize(SpaceshipPart part)
    {
        this.part = part;
    }


}
