using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CraftingStationUITip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stationName;
    [SerializeField] private TextMeshProUGUI stationDescription;
    [SerializeField] private RawImage stationBackground;

    [SerializeField] private Transform inputParent;
    [SerializeField] private Transform outputParent;

    [SerializeField] private Transform leftClickIcon;
    [SerializeField] private Transform parent;

    [SerializeField] private CraftingStationItemUI craftingStationItemUIPrefab;

    public void Initialize()
    {
        for (int i = 0; i < inputParent.childCount; i++)
            Destroy(inputParent.GetChild(i).gameObject);

        for (int i = 0; i < outputParent.childCount; i++)
            Destroy(outputParent.GetChild(i).gameObject);
    }

    public void ShowTipForStation(CraftingStation craftingStation)
    {
        StartCoroutine(Show(craftingStation));
    }

    IEnumerator Show(CraftingStation craftingStation)
    {
        parent.gameObject.SetActive(true);
        GameManager.gm.mainCanvas.ShowInteractTooltip(false);

        GameManager.gm.canPlayerDoAnything = false;

        stationName.text = craftingStation.name;
        stationBackground.texture = craftingStation.stationBackground;
        stationDescription.text = craftingStation.stationDescription;

        for (int i = 0; i < inputParent.childCount; i++)
            Destroy(inputParent.GetChild(i).gameObject);

        for (int i = 0; i < outputParent.childCount; i++)
            Destroy(outputParent.GetChild(i).gameObject);

        for (int i = 0; i < craftingStation.input.Length; i++)
        {
            CraftingStationItemUI c = Instantiate(craftingStationItemUIPrefab, inputParent);
            c.Initialize(craftingStation.input[i].item, craftingStation.input[i].amount);
        }

        for (int i = 0; i < craftingStation.output.Length; i++)
        {
            CraftingStationItemUI c = Instantiate(craftingStationItemUIPrefab, outputParent);
            c.Initialize(craftingStation.output[i].item, craftingStation.output[i].amount);
        }



        yield return new WaitForSeconds(0.5f);

        leftClickIcon.gameObject.SetActive(true);
        
        while (!Input.GetMouseButtonDown(0))
            yield return 0;

        leftClickIcon.gameObject.SetActive(false);
        parent.gameObject.SetActive(false);
        GameManager.gm.canPlayerDoAnything = true;

        GameManager.gm.mainCanvas.ShowInteractTooltip(true);
    }
}
