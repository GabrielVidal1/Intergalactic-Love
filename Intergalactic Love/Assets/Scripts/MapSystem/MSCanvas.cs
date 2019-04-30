using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSCanvas : MonoBehaviour
{
    [SerializeField] private Slider fuelBar;
    [SerializeField] private Button goButton;

    [SerializeField] private CanvasGroup blackFonduGroup;

    private MapSystem ms;

    public void Initialize(MapSystem ms)
    {
        this.ms = ms;
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
