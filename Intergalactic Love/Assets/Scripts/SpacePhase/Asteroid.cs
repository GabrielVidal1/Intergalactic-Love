using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float damage;

    [Range(10, 100)]
    [SerializeField] private int numberOfFrameToAppear;

    private float finalSize;

    public float GetDamage()
    {
        return damage * finalSize;
    }

    public void Init(float finalSize)
    {
        this.finalSize = finalSize;
        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        transform.localScale = Vector3.zero;

        for (float i = 0; i <= 1f; i+= 1f / (numberOfFrameToAppear * finalSize))
        {
            transform.localScale = Vector3.one * i * finalSize;
            yield return 0;
        }

        transform.localScale = Vector3.one * finalSize;
    }

}
