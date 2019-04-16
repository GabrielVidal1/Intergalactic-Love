using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPHook : MonoBehaviour
{


    private bool aimHook;
    private bool fireHook;

    private float force;
    [SerializeField] private float forceSpeed;
    [SerializeField] private float maxForce;

    [SerializeField] public SpacePhaseManager spacePhaseManager;

    [SerializeField] private Transform hookParent;

    [SerializeField] private Transform hookPreview;
    [SerializeField] private MeshRenderer hookPreviewMr;
    [SerializeField] private Gradient color;

    [SerializeField] private Transform hookHead;

    [SerializeField] private TrailRenderer rope;

    private Vector3 position;
    private LayerMask mask;

    [SerializeField] private float maxLength;
    [SerializeField] private float deploymentSpeed;

    [SerializeField] private float returnSpeed;


    private Vector3 firePosition;

    private void Start()
    {
        mask = LayerMask.GetMask("UI");

        hookPreview.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !fireHook)
        {
            aimHook = true;
            force = 0;
            hookPreview.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0) && aimHook && !fireHook)
        {

            Ray ray = spacePhaseManager.mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000, mask))
            {
                position = hit.point;
                force += Time.deltaTime * forceSpeed;
                hookPreview.localScale = new Vector3(1, 1, force);
                hookParent.rotation = Quaternion.FromToRotation(Vector3.forward, (position - transform.position).normalized);


                hookPreviewMr.material.color = color.Evaluate(force / maxForce);
            }
        }

        if (Input.GetMouseButtonUp(0) && aimHook && !fireHook || force > maxForce)
        {
            ReleaseClick();
        }
    }

    public void ReleaseClick()
    {
        StartCoroutine(FireHook());
        fireHook = true;

        hookPreview.gameObject.SetActive(false);
    }

    IEnumerator FireHook()
    {
        firePosition = transform.position;


        hookParent.rotation = Quaternion.FromToRotation(Vector3.forward, (position - transform.position).normalized);
        hookParent.parent = null;
        float length = 0;

        bool hasHitObject = false;

        hookHead.gameObject.SetActive(true);

        float speedCoef = force / maxForce;

        while (!hasHitObject && length < maxLength)
        {
            hookHead.transform.localPosition = new Vector3(0, 0, length);


            yield return 0;
            length += deploymentSpeed * speedCoef * Time.deltaTime;

        }
        StartCoroutine(DrawRope());

        Vector3 pos = hookHead.transform.position;
        hookParent.transform.position = pos;
        hookHead.transform.localPosition = Vector3.zero;

        float k = 0f;


        while (k < 1f)
        {
            hookParent.transform.position = Vector3.Lerp(pos, transform.position, k);
            yield return 0;
            k += 0.01f;
        }

        hookHead.gameObject.SetActive(false);

        hookParent.parent = transform;
        hookParent.localPosition = Vector3.zero;

        fireHook = false;
    }

    IEnumerator DrawRope()
    {
        rope.transform.position = transform.position;
        rope.emitting = true;

        for (float i = 0f; i < 1f; i += 0.1f)
        {
            rope.transform.position = transform.position * i * i +
                firePosition * 2 * i * (1 - i) +
                hookHead.transform.position * (1f - i) * (1f - i);
            yield return 0;
        }

        rope.transform.position = hookHead.transform.position;

        rope.emitting = false;

    }


}
