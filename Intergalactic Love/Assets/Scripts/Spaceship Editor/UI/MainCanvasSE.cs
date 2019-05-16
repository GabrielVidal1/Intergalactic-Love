using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject[] tips;
    [SerializeField] private GameObject leftClickIcon;
    [SerializeField] private GameObject leftClickIconText;

    [SerializeField] private CanvasGroup group;

    public Button goButton;

    public void ClickOnGoButton()
    {
        StartCoroutine(GoInSpace());
    }

    IEnumerator GoInSpace()
    {
        spaceshipSaveLoad.SaveSpaceship();

        for (float i = 0f; i < 1f; i += Time.deltaTime)
        {
            group.alpha = i;
            yield return 0;
        }
        group.alpha = 1f;

        SceneManager.LoadScene("Itinerary");
    }

    private void Start()
    {
        GameManager.gm.mainCanvasSE = this;

        partList.Initialize();
        toolbar.Initialize();
        GameManager.gm.player.DisablePlayer();
        SetUpCamera();

        spaceshipSaveLoad.LoadSpaceship();

        goButton.interactable = false;

        StartCoroutine(ShowTips());
    }

    IEnumerator ShowTips()
    {
        GameManager.gm.canPlayerDoAnything = false;

        foreach (GameObject tip in tips)
        {
            tip.SetActive(true);
            yield return new WaitForSeconds(2f);

            leftClickIcon.SetActive(true);

            while (Input.GetMouseButtonDown(0))
                yield return 0;
            while (!Input.GetMouseButtonDown(0))
                yield return 0;

            leftClickIcon.SetActive(false);
            tip.SetActive(false);
            leftClickIconText.SetActive(false);
        }

        GameManager.gm.canPlayerDoAnything = true;
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