using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : Attractor
{
    public Vector3 Center
    { get { return transform.position; } }

    Vector3 center1;
    Vector3 center2;
    float dist;

    private void Start()
    {
        center1 = Center - transform.up * 0.5f * transform.localScale.y;
        center2 = Center + transform.up * 0.5f * transform.localScale.y;
        dist = Vector3.Distance(center1, center2);
    }

    protected override Vector3 GetUp(Transform target)
    {
        if (Vector3.Dot(transform.up, target.position - center1) <= 0)
        {
            return (target.position - center1).normalized;
        }
        Vector3 proj = Vector3.Project(target.position - center1, transform.up);

        float d = Mathf.Clamp01((dist - proj.magnitude)/ dist);
        return (target.position - Vector3.Lerp(center2, center1, d)).normalized;
    }

}
 