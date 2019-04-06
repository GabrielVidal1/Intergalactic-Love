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
        Structure,
        Reactor,
        Cockpit,
        Weapon
    }

    public bool canBeParent;
    public bool canBeRotated;

    public SpaceshipPartMesh mesh;

    private List<SpaceshipPart> attachedParts;
    private SpaceshipPart parent;

    private SpaceshipPartMesh mesh2;

    private Vector3 position;
    private Quaternion rotation;

    public void Initialize()
    {
        mesh2 = Instantiate(mesh);
        mesh2.transform.SetParent(transform);
        mesh.Initiliaze(this);
        mesh2.Initiliaze(this);
    }

    public void SetMat(Material mat)
    {
        mesh.SetMat(mat);
        mesh2.SetMat(mat);
    }

    public void DisableSymetry(bool disable)
    {
        if (mesh2 != null)
            mesh2.gameObject.SetActive(disable);
    }

    #region Spaceship Manipulation

    public void AttachToPart(SpaceshipPart parent)
    {
        this.parent = parent;
        transform.parent = parent.transform;

        GameManager.gm.mainCanvasSE.partTransformation.SelectePart(this);
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

    public SerializedSpaceship.SerializedSpaceshipPart SerializePart()
    {
        SerializedSpaceship.SerializedSpaceshipPart sPart = new SerializedSpaceship.SerializedSpaceshipPart();

        int nb = attachedParts.Count;
        sPart.parts = new SerializedSpaceship.SerializedSpaceshipPart[nb];
        sPart.position = position;
        sPart.rotation = rotation;

        for (int i = 0; i < nb; i++)
        {
            sPart.parts[i] = attachedParts[i].SerializePart();
        }

        return sPart;
    }

    #endregion

    #region Mesh Manipulation

    public void SetPosition(Vector3 pos, bool isPosValid)
    {
        //POSITION IN X IN ALWAYS POSITIVE !!!
        if (isPosValid)
           pos.x = Mathf.Abs(pos.x);

        position = pos;
        mesh.transform.position = pos;
        Vector3 pos2 = pos;
        pos2.x *= -1;
        mesh2.transform.position = pos2;
    }

    public void LookAt(Vector3 direction)
    {
        //DIRECTION IN X IN ALWAYS POSITIVE !!!
        direction.x = Mathf.Abs(direction.x);

        rotation = Quaternion.LookRotation(direction);

        mesh.transform.rotation = rotation;

        Quaternion rot2 = rotation;
        rot2.y *= -1;
        rot2.z *= -1;

        mesh2.transform.rotation = rot2;
    }

    #endregion
}
