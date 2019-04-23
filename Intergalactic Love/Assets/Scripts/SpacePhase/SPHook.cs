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

    [SerializeField] private Transform hookPreview;
    [SerializeField] private MeshRenderer hookPreviewMr;
    [SerializeField] private Gradient color;

    [SerializeField] private float hookHeadRadius;
    [SerializeField] private Transform hookHead;


    [SerializeField] private float maxLength;
    [SerializeField] private float deploymentSpeed;
    [SerializeField] private float returnSpeed;

    [SerializeField] private AnimationCurve hitWreckSizeOverTime;

    private LayerMask mask;

    private Vector3 firePosition;

    private Vector3 fireDirection;

    private SEWreck hitMesh;

    private void Start()
    {
        mask = LayerMask.GetMask("UI");

        hookPreview.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !fireHook && !aimHook)
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
                force += Time.deltaTime * forceSpeed;

                fireDirection = (hit.point - transform.position).normalized;

                hookPreview.localScale = new Vector3(1, 1, force);
                hookPreview.rotation = Quaternion.FromToRotation(Vector3.forward, fireDirection);


                hookPreviewMr.material.color = color.Evaluate(force / maxForce);
            }
        }

        if (Input.GetMouseButtonUp(0) && aimHook && !fireHook || force > maxForce)
        {
            StartCoroutine(FireHook());
            fireHook = true;
            aimHook = false;

            hookPreview.gameObject.SetActive(false);
        }
    }

    IEnumerator FireHook()
    {
        firePosition = transform.position;

        float length = 0;
        bool hasHitObject = false;

        hookHead.gameObject.SetActive(true);

        float speedCoef = force / maxForce;

        while (!hasHitObject && length < maxLength)
        {
            hookHead.transform.position = firePosition + fireDirection * length;

            foreach (Collider collider in Physics.OverlapSphere(hookHead.transform.position, hookHeadRadius))
            {
                if (collider.CompareTag("SpaceshipWreck"))
                {
                    SEWreck wreck = collider.transform.parent.GetComponent<SEWreck>();
                    wreck.transform.SetParent(hookHead);
                    hasHitObject = true;

                    hitMesh = wreck;

                    Debug.Log("CAUGHT");

                    break;
                }
            }

            yield return 0;
            length += deploymentSpeed * speedCoef * Time.deltaTime;

        }

        Vector3 extremumPos = hookHead.transform.position;
        hookHead.transform.position = extremumPos;

        float k = 0f;

        while (k < 1f)
        {
            hookHead.transform.position = Vector3.Lerp(extremumPos, transform.position, k);

            if (hasHitObject)
            {
                hitMesh.transform.localScale = Vector3.one * hitWreckSizeOverTime.Evaluate(k / 0.9f);
            }

            yield return 0;
            k += 0.01f;
        }

        fireHook = false;
        hookHead.gameObject.SetActive(false);


        if (hasHitObject)
        {
            hitMesh.CollectItems();
        }
    }



}
