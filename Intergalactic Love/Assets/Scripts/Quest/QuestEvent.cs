using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestEvent : MonoBehaviour
{
    public Transform cameraInterestPoint;

    public bool shouldAnimate = false;
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

        GameManager.gm.canPlayerDoAnything = false;

        Camera mainCam = GameManager.gm.player.mainCam;

        Vector3 initialPosition = mainCam.transform.position;
        Quaternion initialRotation = mainCam.transform.rotation;

        for (float i = 0f; i < 1f; i += 1f / framePerTravel)
        {
            mainCam.transform.position = Vector3.Lerp(initialPosition, cameraInterestPoint.position, i);
            mainCam.transform.rotation = Quaternion.Lerp(initialRotation, cameraInterestPoint.rotation, i);
            yield return 0;
        }

        mainCam.transform.position = cameraInterestPoint.position;
        mainCam.transform.rotation = cameraInterestPoint.rotation;

        yield return new WaitForSeconds(waitAtEventDuration/2);

        GameManager.gm.soundManager.PlaySound(sound);

        yield return StartCoroutine(Execute());


        yield return new WaitForSeconds(waitAtEventDuration/2);

        for (float i = 0f; i < 1f; i += 1f / framePerTravel)
        {
            mainCam.transform.position = Vector3.Lerp(initialPosition, cameraInterestPoint.position, 1 - i);
            mainCam.transform.rotation = Quaternion.Lerp(initialRotation, cameraInterestPoint.rotation, 1 - i);
            yield return 0;
        }

        mainCam.transform.position = initialPosition;
        mainCam.transform.rotation = initialRotation;

        GameManager.gm.canPlayerDoAnything = true;
    }
}
