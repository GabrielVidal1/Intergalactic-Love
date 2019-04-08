using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationGizmo : MonoBehaviour
{
    private SpaceshipPart part;
    [SerializeField] private GameObject rotationPlane;
    [SerializeField] private MeshRenderer mr;

    [SerializeField] private Material defaultRotationGizmoMat;
    [SerializeField] private Material selectedRotationGizmoMat;

    public void Initialize(SpaceshipPart part)
    {
        this.part = part;
        rotationPlane.SetActive(false);
    }

    public void SetIsRotating(bool active)
    {
        rotationPlane.SetActive(active);
        mr.material = active ? selectedRotationGizmoMat : defaultRotationGizmoMat;
    }

}
