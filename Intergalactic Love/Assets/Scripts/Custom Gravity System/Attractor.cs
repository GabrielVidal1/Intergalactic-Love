using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attractor : MonoBehaviour
{
    protected virtual Vector3 GetUp(Transform target)
    {
        throw new UnityException("not good");
    }

    public Vector3 Attract(Transform target)
    {
        Vector3 up = GetUp(target);

        Quaternion targetRotation = Quaternion.FromToRotation(target.up, up) * target.rotation;
        target.rotation = Quaternion.Slerp(target.rotation, targetRotation, 0.1f);

        return up;
    }
}

