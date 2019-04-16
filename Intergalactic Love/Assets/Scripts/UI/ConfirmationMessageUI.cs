using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class ConfirmationMessageUI : MonoBehaviour
{
    [SerializeField] private Transform parent;

    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI goText;

    private Action onClickGo; 
    private Action onClickCancel;

    public void OnClickGo()
    {
        parent.gameObject.SetActive(false);

        onClickGo.Invoke();
    }

    public void OnClickCancel()
    {
        parent.gameObject.SetActive(false);

        onClickCancel.Invoke();
    }

    public void TriggerMessage(string question, string goResponse,
                                Action go, Action cancel)
    {
        questionText.text = question;
        goText.text = goResponse;
        onClickGo = go;
        onClickCancel = cancel;
        parent.gameObject.SetActive(true);
    }

    public void Initiliaze()
    {
        parent.gameObject.SetActive(false);
    }
}
