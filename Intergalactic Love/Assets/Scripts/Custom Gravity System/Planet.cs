using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Attractor
{
    [SerializeField] private Vector3 center;
    [SerializeField] private bool isCenterTransformPosition;
    public Vector3 Center
    {
        get
        {
            return transform.position + (isCenterTransformPosition ? Vector3.zero : center);
        }
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
