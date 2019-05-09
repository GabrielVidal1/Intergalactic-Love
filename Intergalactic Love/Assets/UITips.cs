using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITips : MonoBehaviour
{
    [SerializeField] private GameObject clickToNext;

    [Header("Tips")]
    [SerializeField] private Tip[] tips;

    [System.Serializable]
    public class Tip
    {
        public string name;
        public GameObject tipObject;
    }

    private Dictionary<string, GameObject> tipsDic;

    public void Initialize()
    {
        tipsDic = new Dictionary<string, GameObject>();
        foreach (Tip tip in tips)
            tipsDic.Add(tip.name, tip.tipObject);
    }


    public IEnumerator ShowTipCoroutine(string tipName)
    {
        GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.tipOn);

        GameObject tip = tipsDic[tipName];

        tip.SetActive(true);

        GameManager.gm.canPlayerDoAnything = false;

        yield return new WaitForSecondsRealtime(1f);

        clickToNext.SetActive(true);

        while (!Input.GetMouseButtonDown(0))
            yield return 0;

        GameManager.gm.soundManager.PlaySound(GameManager.gm.soundManager.tipOff);

        clickToNext.SetActive(false);
        tip.SetActive(false);
        GameManager.gm.canPlayerDoAnything = true;

    }
}
