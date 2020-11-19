using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestEvent : MonoBehaviour
{
    public Transform cameraInterestPoint;

    public bool shouldAnimate = false;

    public Transform intermediatePos;
    public float waitAtEventDuration = 1.5f;

    [Range(1, 100)]
    public int framePerTravel = 40;

    [SerializeField] private AudioClip sound;

    protected abstract IEnumerator Execute();

    public IEnumerator Invoke()
    {
        if (!shouldAnimate)
        {
            yield return StartCoroutine(Execute());
            yield break;
        }

        GameManager.gm.mainCanvas.CloseOpenedUi();
        GameManager.gm.canPlayerDoAnything = false;
        Camera mainCam = GameManager.gm.player.mainCam;

        Vector3 initialPosition = mainCam.transform.position;
        Quaternion initialRotation = mainCam.transform.rotation;

        for (float i = 0f; i < 1f; i += 1f / framePerTravel)
        {
            if (intermediatePos != null)
            {
                if (i < 0.5f)
                {
                    mainCam.transform.position = Vector3.Lerp(initialPosition, intermediatePos.position, 2f * i);
                }
                else
                {
                    mainCam.transform.position = Vector3.Lerp(intermediatePos.position, cameraInterestPoint.position, 2f * (i - 0.5f));
                }
            }
            else
            {
                mainCam.transform.position = Vector3.Lerp(initialPosition, cameraInterestPoint.position, i);
            }


            mainCam.transform.rotation = Quaternion.Lerp(initialRotation, cameraInterestPoint.rotation, i);
            yield return 0;
        }

        mainCam.transform.position = cameraInterestPoint.position;
        mainCam.transform.rotation = cameraInterestPoint.rotation;

        Transform initialParent = mainCam.transform.parent;
        mainCam.transform.SetParent(cameraInterestPoint);

        yield return new WaitForSeconds(waitAtEventDuration/2);
        GameManager.gm.soundManager.PlaySound(sound);
        yield return StartCoroutine(Execute());

        mainCam.transform.SetParent(initialParent);

        yield return new WaitForSeconds(waitAtEventDuration/2);

        for (float i = 1f; i >= 0f; i -= 1f / framePerTravel)
        {
            if (intermediatePos != null)
            {
                if (i < 0.5f)
                {
                    mainCam.transform.position = Vector3.Lerp(initialPosition, intermediatePos.position, 2f * i);
                }
                else
                {
                    mainCam.transform.position = Vector3.Lerp(intermediatePos.position, cameraInterestPoint.position, 2f * (i- 0.5f));
                }
            }
            else
            {
                mainCam.transform.position = Vector3.Lerp(initialPosition, cameraInterestPoint.position, i);
            }

            mainCam.transform.rotation = Quaternion.Lerp(initialRotation, cameraInterestPoint.rotation, i);
            yield return 0;
        }

        mainCam.transform.position = initialPosition;
        mainCam.transform.rotation = initialRotation;

        GameManager.gm.canPlayerDoAnything = true;
    }
}
