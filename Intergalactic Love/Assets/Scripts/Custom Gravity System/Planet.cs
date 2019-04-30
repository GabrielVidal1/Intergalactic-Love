using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Attractor
{
    public Vector3 Center
    {
        get { return transform.position; }
    }

    protected override Vector3 GetUp(Transform target)
    {
        return (target.position - Center).normalized;
    }

    public static void Orient(Transform obj, Planet planet)
    {
        Vector3 localUp = (obj.position - planet.Center).normalized;
        Quaternion rot = Quaternion.FromToRotation(obj.up, localUp);
        obj.rotation = rot;
    }
}
