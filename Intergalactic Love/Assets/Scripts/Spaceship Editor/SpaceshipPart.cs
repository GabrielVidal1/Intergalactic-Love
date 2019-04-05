using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPart : MonoBehaviour
{
    public ItemData itemData;

    public SpaceshipPartType type;
    public enum SpaceshipPartType
    {
        Core,
        Reactor,
        Cockpit,
        Weapon
    }

    public bool canBeParent;

    [SerializeField] private SpaceshipPartMesh mesh;

    private List<SpaceshipPart> attachedParts;
    private SpaceshipPart parent;

    private SpaceshipPartMesh mesh2;

    public void Initialize()
    {
        mesh2 = Instantiate(mesh);
    }

    public void SetMat(Material mat)
    {
        mesh.SetMat(mat);
        mesh2.SetMat(mat);
    }

    #region Spaceship Manipulation

    public void AttachToPart(SpaceshipPart parent)
    {
        this.parent = parent;
    }

    public void Remove()
    {
        foreach (SpaceshipPart part in attachedParts)
        {
            part.Remove();
        }
        Destroy(gameObject);
    }

    public void RemovePart(Spaceship part)
    {
        attachedParts.Remove(part);
    }

    public void AddPart(Spaceship part)
    {
        attachedParts.Add(part);
        part.transform.SetParent(transform);
    }

    #endregion

    #region Mesh Manipulation

    public void SetPosition(Vector3 pos)
    {
        mesh.transform.position = pos;
        Vector3 pos2 = pos;
        pos2.x *= -1;
        mesh2.transform.position = pos2;
    }

    public void LookAt(Vector3 direction)
    {
        mesh.transform.LookAt(mesh.transform.position + direction);
        Vector3 dir2 = direction;
        dir2.x *= -1;
        mesh2.transform.LookAt(mesh2.transform.position + dir2);
    }

    #endregion
}
