using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainCanvasSE : MonoBehaviour
{
    public PartList partList;
    public Camera viewCam;
    public Camera mainCam;
    public SEToolbar toolbar;

    public SpaceshipSaveLoad spaceshipSaveLoad;

    public RawImage viewRawImage;

    public PartTransformation partTransformation;

    [SerializeField] public Material notValidMat;

    private void Start()
    {
        partList.Initialize();
        toolbar.Initialize();
        GameManager.gm.player.DisablePlayer();
        SetUpCamera();

        spaceshipSaveLoad.LoadSpaceship();
    }

    private void SetUpCamera()
    {
        RenderTexture rt = new RenderTexture((int)viewRawImage.rectTransform.rect.width,
                                  (int)viewRawImage.rectTransform.rect.height, 16);

        viewCam.targetTexture = rt;
        viewRawImage.texture = rt;
    }

    public Ray GetRayFromMousePosition()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        //Debug.Log("rect = " + viewRawImage.rectTransform.rect);

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (viewRawImage.rectTransform, Input.mousePosition, null, out localPoint))
        {

            localPoint.x = 0.5f + localPoint.x / viewRawImage.rectTransform.rect.width;
            localPoint.y = 0.5f + localPoint.y / viewRawImage.rectTransform.rect.height;

            ray = viewCam.ViewportPointToRay(localPoint);

        }
        return ray;
    }
}