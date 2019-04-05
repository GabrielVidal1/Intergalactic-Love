using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Attractor
{
    protected override Vector3 GetUp(Transform target)
    {
        return transform.up;
    }
}
