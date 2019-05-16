using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MSCanvas : MonoBehaviour
{
    [SerializeField] private Slider fuelBar;
    [SerializeField] private Button goButton;

    [SerializeField] private CanvasGroup blackFonduGroup;

    [SerializeField] private GameObject[] tips;
    [SerializeField] private GameObject finalTip;
    [SerializeField] private TextMeshProUGUI finalTipText;

    [SerializeField] private GameObject leftClickIcon;
    [SerializeField] private GameObject leftClickIconText;

    public GameObject playerTarget;
    public GameObject planet2Target;

    private MapSystem ms;

    public void Initialize(MapSystem ms)
    {
        this.ms = ms;
    }

    public IEnumerator ShowTips()
    {
        playerTarget.SetActive(false);
        planet2Target.SetActive(false);

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

        yield return StartCoroutine(ShowDumbassTip());
    }

    public IEnumerator ShowDumbassTip()
    {
        finalTip.SetActive(true);
        playerTarget.SetActive(true);
        planet2Target.SetActive(true);

        yield return new WaitForSeconds(2f);

        leftClickIcon.SetActive(true);

        while (Input.GetMouseButtonDown(0))
            yield return 0;
        while (!Input.GetMouseButtonDown(0))
            yield return 0;

        leftClickIcon.SetActive(false);
        finalTip.SetActive(false);
        leftClickIconText.SetActive(false);
        planet2Target.SetActive(false);

        finalTipText.text = "You need to go to the second planet, not wander in space!";
    }

    public void SetFuel(float value)
    {
        fuelBar.value = 1f - value;
    }

    public void SetGoButtonInteractible(bool interactible)
    {
        goButton.interactable = interactible;
    }

    public void OnClickGo()
    {
        GameManager.gm.currentItinerary = ms.itineraryTracer.GetItinerary(Itinerary.MapPoint.Event.Planet2);

        StartCoroutine(blackFondu(true, MapSystem.SceneName.SpacePhase));
    }

    public void OnClickBack()
    {
        StartCoroutine(blackFondu(true, MapSystem.SceneName.Planet));
    }

    IEnumerator blackFondu(bool toBlack, MapSystem.SceneName sceneName)
    {
        for (float i = 0f; i < 1f; i += 0.02f)
        {
            blackFonduGroup.alpha = toBlack ? i : 1f - i;
            yield return 0;
        }
        blackFonduGroup.alpha = toBlack ? 1f : 0f;
        ms.ChangeScene(sceneName);
    }
}
