using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPartMesh : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private MeshRenderer mr;

    public void SetMat(Material mat)
    {
        if (mat == null)
            mr.material = defaultMaterial;
        else
            mr.material = mat;
    }
}
