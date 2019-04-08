using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPart : MonoBehaviour
{
    #region Accessible Variables
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
    public bool canBeMoved;

    public bool isSymetric;

    public SpaceshipPartMesh mesh;

    //[HideInInspector]
    public int index;
    
    #endregion

    [SerializeField]
    private List<SpaceshipPart> attachedParts;
    private SpaceshipPart parent;

    public void SetParentPart(SpaceshipPart parent)
    {
        this.parent = parent;
        transform.parent = parent.mesh.transform;
    }

    private SpaceshipPartMesh mesh2;

    public void Initialize()
    {
        transform.position = Vector3.zero;

        mesh2 = Instantiate(mesh);
        mesh2.transform.SetParent(transform);
        mesh.Initiliaze(this);
        mesh2.Initiliaze(this);

        mesh2.gameObject.SetActive(isSymetric);

        attachedParts = new List<SpaceshipPart>();
    }

    public void SetMat(Material mat)
    {
        mesh.SetMat(mat);
        mesh2.SetMat(mat);
    }

    public void DisableSymetry(bool disable)
    {
        if (mesh2 != null)
            mesh2.gameObject.SetActive(isSymetric && disable);
    }

    #region Spaceship Manipulation

    public void DetachFromParent()
    {
        parent.RemovePart(this);
        parent = null;
    }

    public void AttachToPart(SpaceshipPart parent)
    {
        parent.AddPart(this);

        this.parent = parent;

        mesh.DisableCollider(false);
        mesh2.DisableCollider(false);

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

    public void RemovePart(SpaceshipPart part)
    {
        attachedParts.Remove(part);
    }

    public void AddPart(SpaceshipPart part)
    {
        attachedParts.Add(part);
        part.SetParentPart(this);
    }

    #endregion

    #region Serialization

    public SpaceshipSaveLoad.SerializedSpaceshipPart SerializePart()
    {
        SpaceshipSaveLoad.SerializedSpaceshipPart sPart = new SpaceshipSaveLoad.SerializedSpaceshipPart();

        int nb = attachedParts.Count;
        sPart.parts = new SpaceshipSaveLoad.SerializedSpaceshipPart[nb];
        sPart.position = new SpaceshipSaveLoad.SerializedSpaceshipPart.SerializedVector3(mesh.transform.position);
        sPart.rotation = new SpaceshipSaveLoad.SerializedSpaceshipPart.SerializedQuaternion(mesh.transform.rotation);
        sPart.partIndex = index;

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

        mesh.transform.position = pos;
        Vector3 pos2 = mesh.transform.position;
        pos2.x *= -1;
        mesh2.transform.position = pos2;

        UpdatedChildrenParts();
    }

    public void SetRotation(Quaternion rotation)
    {
        mesh.transform.rotation = rotation;
        Quaternion rot2 = mesh.transform.rotation;
        rot2.y *= -1;
        rot2.z *= -1;

        mesh2.transform.rotation = rot2;

        UpdatedChildrenParts();
    }

    public void RotateAroundZAxis(Vector3 fromDir, Vector3 toDir)
    {
        Quaternion targetRot = Quaternion.FromToRotation(fromDir, toDir);

        mesh.transform.rotation = Quaternion.Slerp(mesh.transform.rotation,
            targetRot, 1f) * mesh.transform.rotation;

        Quaternion rot2 = mesh.transform.rotation;
        rot2.y *= -1;
        rot2.z *= -1;

        mesh2.transform.rotation = rot2;

        UpdatedChildrenParts();
    }

    public void UpdatedChildrenParts()
    {
        foreach (SpaceshipPart part in attachedParts)
        {
            part.UpdatePositionAndRotation();
        }
    }

    public void UpdatePositionAndRotation()
    {
        print("UpdatePositionAndRotation()");

        Quaternion rot2 = mesh.transform.rotation;
        rot2.y *= -1;
        rot2.z *= -1;

        mesh2.transform.rotation = rot2;

        Vector3 pos2 = mesh.transform.position;
        pos2.x *= -1;
        mesh2.transform.position = pos2;
    }

    #endregion

    public void DisableCollider(bool disable)
    {
        mesh.DisableCollider(disable);
        mesh2.DisableCollider(disable);
        foreach (SpaceshipPart childPart in attachedParts)
        {
            childPart.DisableCollider(disable);
        }
    }

    public void Destroy()
    {
        GameManager.gm.mainCanvasSE.partList.listParts[itemData].PreviewQuantity++;

        SpaceshipPart[] p = attachedParts.ToArray();
        foreach (SpaceshipPart part in p)
        {
            part.Destroy();
        }
        parent.RemovePart(this);
        Destroy(gameObject);
    }
}
