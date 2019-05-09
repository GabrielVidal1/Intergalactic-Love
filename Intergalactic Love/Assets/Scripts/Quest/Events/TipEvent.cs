using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipEvent : QuestEvent
{
    [SerializeField] private string tipName;

    protected override IEnumerator Execute()
    {
        yield return StartCoroutine(GameManager.gm.mainCanvas.uiTips.ShowTipCoroutine(tipName));
    }
}
