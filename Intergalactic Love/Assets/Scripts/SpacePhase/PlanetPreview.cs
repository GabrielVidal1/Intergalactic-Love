using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPreview : MonoBehaviour
{
    [Range(10, 100)]
    [SerializeField] private int numberOfFrameToAppear;

    public void Init(float finalSize)
    {
        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        transform.localScale = Vector3.zero;

        for (float i = 0; i <= 1f; i += 1f / (numberOfFrameToAppear))
        {
            transform.localScale = Vector3.one * i;
            yield return 0;
        }

        transform.localScale = Vector3.one;
    }

}
