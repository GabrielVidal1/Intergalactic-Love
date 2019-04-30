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
}
