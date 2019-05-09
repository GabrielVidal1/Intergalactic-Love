using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public RectTransform map;
    public RectTransform mapParent;

    float size;

    public void SetUp(Itinerary itinerary)
    {
        size = map.rect.width;

        Vector3 p1 = itinerary.points[0].position;
        Vector3 p2 = itinerary.points[1].position;

        mapParent.rotation = Quaternion.FromToRotation(new Vector3(p2.x - p1.x, p2.z - p1.z, 0), Vector3.up);
        map.anchoredPosition = new Vector2(0.5f - p1.x, 0.5f - p1.z) * size;
    }

    public void SetPosition(Itinerary itinerary, int index, float duration)
    {
        print("Set pos  " + index);
        if (index >= itinerary.points.Count) return;

        StartCoroutine(Smooth(itinerary, index, duration / Time.deltaTime));
    }

    IEnumerator Smooth(Itinerary itinerary, int index, float frameNumber)
    {
        Vector3 p1 = itinerary.points[index].position;
        Vector3 p2 = itinerary.points[index + 1].position;

        Quaternion initRot = mapParent.rotation;
        Quaternion targetRot = Quaternion.FromToRotation(new Vector3(p2.x - p1.x, p2.z - p1.z, 0), Vector3.up);

        Vector2 initPos = map.anchoredPosition;
        Vector2 targetPos = new Vector2(0.5f - p1.x, 0.5f - p1.z) * size;

        for (float i = 0f; i <= 1f; i+= 1f / frameNumber)
        {

            map.anchoredPosition = Vector2.Lerp(initPos, targetPos, i);
            mapParent.rotation = Quaternion.Lerp(initRot, targetRot, i);
            yield return 0;
        }


    }
}
