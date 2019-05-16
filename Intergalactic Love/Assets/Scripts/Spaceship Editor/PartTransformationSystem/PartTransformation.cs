using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartTransformation : MonoBehaviour
{
    private SpaceshipPart selectedPart;

    public RotationGizmo rotationGizmo;

    private bool isRotating = false;
    private bool isDraggingPart = false;
    private Vector3 refDir;

    private SpaceshipPart parentPart;

    private Vector3 initialMousePos;

    private LayerMask mask;

    private void Start()
    {
        rotationGizmo.gameObject.SetActive(false);
        mask = LayerMask.GetMask("SpaceshipEditor");
    }

    private void Update()
    {
        if (!GameManager.gm.canPlayerDoAnything) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GameManager.gm.mainCanvasSE.GetRayFromMousePosition();
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, mask))
            {
                print("click : " + hit.collider.tag);

                if (hit.collider.CompareTag("RotationGizmo"))
                {
                    isRotating = true;
                    refDir = Vector3.ProjectOnPlane(hit.point - rotationGizmo.transform.position, rotationGizmo.transform.forward).normalized;
                    Debug.Log("refdir = " + refDir);
                    rotationGizmo.SetIsRotating(true);
                    print("INIT");
                    return;

                }
            }
            else if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("SpaceshipPart"))
                {
                    selectedPart = hit.collider.GetComponent<SpaceshipPartMesh>().GetPart();
                    if (selectedPart.canBeMoved)
                        SelectePart(selectedPart);
                    else
                        selectedPart = null;

                    initialMousePos = Input.mousePosition;
                    return;
                }
            }
            selectedPart = null;
            rotationGizmo.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = GameManager.gm.mainCanvasSE.GetRayFromMousePosition();
            RaycastHit hit;

            if (isRotating)
            {
                print("ROTATION");
                
                if (Physics.Raycast(ray, out hit, 1000f, mask))
                {
                    //print(hit.collider.tag);
                    if (hit.collider.CompareTag("RotationGizmo"))
                    {
                        Vector3 dir = Vector3.ProjectOnPlane(hit.point - rotationGizmo.transform.position, rotationGizmo.transform.forward).normalized;

                        selectedPart.RotateAroundZAxis(refDir, dir);

                        refDir = dir;
                    }
                }
            }
            else if (selectedPart != null && !isDraggingPart)
            {
                float v = Vector3.SqrMagnitude(Input.mousePosition - initialMousePos);
                print(v);

                if (v > 10)
                {
                    isDraggingPart = true;
                    rotationGizmo.gameObject.SetActive(false);
                    selectedPart.DisableCollider(true);
                }
            }
            if (isDraggingPart)
            {
                print("DRAGGING");


                if (Physics.Raycast(ray, out hit))
                {
                    bool isPosValid = true;
                    if (hit.collider.CompareTag("SpaceshipPart") &&
                        hit.collider.GetComponent<SpaceshipPartMesh>().GetPart().canBeParent)
                    {
                        
                        selectedPart.SetMat(null);
                        parentPart = hit.collider.GetComponent<SpaceshipPartMesh>().GetPart();
                    }
                    else
                    {
                        selectedPart.SetMat(GameManager.gm.mainCanvasSE.notValidMat);
                        parentPart = null;
                        isPosValid = false;
                    }

                    selectedPart.DisableSymetry(isPosValid);
                    selectedPart.SetPosition(hit.point, isPosValid);
                    Vector3 normal = - hit.normal;
                    normal.x = -Mathf.Abs(normal.x);
                    selectedPart.SetRotation(Quaternion.LookRotation(normal));
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isRotating)
            {
                rotationGizmo.SetIsRotating(false);
                isRotating = false;
            }

            if (isDraggingPart)
            {
                Ray ray = GameManager.gm.mainCanvasSE.GetRayFromMousePosition();
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (parentPart != null)
                    {
                        selectedPart.AttachToPart(parentPart);
                    }
                    else
                    {
                        rotationGizmo.transform.SetParent(transform);
                        selectedPart.Destroy();
                    }
                }
                isDraggingPart = false;
            }
        }
    }

    public void SelectePart(SpaceshipPart part)
    {
        print("select part : " + part.name);
        if (part.canBeMoved)
        {
            selectedPart = part;

            if (selectedPart.canBeRotated)
            {
                rotationGizmo.gameObject.SetActive(true);
                rotationGizmo.Initialize(selectedPart);

                rotationGizmo.transform.SetParent(selectedPart.mesh.transform);
                rotationGizmo.transform.localPosition = Vector3.zero;
                rotationGizmo.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
