using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPartMesh : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private MeshRenderer parent;

    [SerializeField] private List<GameObject> displayWhenGood;

    private SpaceshipPart part;
    public SpaceshipPart GetPart()
    { return part; }

    private Collider col;

    public void Initiliaze(SpaceshipPart part)
    {
        this.part = part;
        col = GetComponent<Collider>();
    }

    public void ReflectAlongZ()
    {
        Vector3 scale = parent.transform.localScale;
        scale.z *= - 1f;
        parent.transform.localScale = scale;
    }

    public void SetMat(Material mat)
    {
        if (mat == null)
        {
            parent.material = defaultMaterial;
        }
        else
        {
            parent.material = mat;
        }
    }

    public void DisableCollider(bool disable)
    {
        col.enabled = !disable;
        foreach (GameObject obj in displayWhenGood)
        {
            obj.SetActive(!disable);
        }
    }
}
