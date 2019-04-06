using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartTransformation : MonoBehaviour
{
    private SpaceshipPart selectedPart;

    public RotationGizmo rotationGizmo;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GameManager.gm.mainCanvasSE.mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("SpaceshipPart"))
                {
                    selectedPart = hit.collider.GetComponent<SpaceshipPartMesh>().GetPart();
                    if (selectedPart.canBeRotated)
                    {
                        SelectePart(selectedPart);
                    }
                    else
                    {
                        selectedPart = null;
                    }
                }
                else
                {
                    selectedPart = null;
                    rotationGizmo.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SelectePart(SpaceshipPart part)
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
