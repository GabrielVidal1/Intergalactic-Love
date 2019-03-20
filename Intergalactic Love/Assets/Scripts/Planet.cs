using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Attractor
{
    protected override Vector3 GetUp(Transform target)
    {
        return (target.position - transform.position).normalized;
    }
}
