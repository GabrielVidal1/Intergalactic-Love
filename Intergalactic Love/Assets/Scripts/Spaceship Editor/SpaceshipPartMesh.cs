using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPartMesh : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;

    private MeshRenderer mr;

    private SpaceshipPart part;
    public SpaceshipPart GetPart()
    { return part; }

    public void Initiliaze(SpaceshipPart part)
    {
        this.part = part;
        mr = GetComponent<MeshRenderer>();
    }

    public void SetMat(Material mat)
    {
        if (mat == null)
            mr.material = defaultMaterial;
        else
            mr.material = mat;
    }
}
