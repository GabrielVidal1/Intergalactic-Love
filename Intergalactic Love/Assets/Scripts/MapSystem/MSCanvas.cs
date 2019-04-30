using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSCanvas : MonoBehaviour
{
    [SerializeField] private Slider fuelBar;

    private MapSystem ms;

    public void Initialize(MapSystem ms)
    {
        this.ms = ms;
    }

    public void SetFuel(float value)
    {
        fuelBar.value = 1f - value;
    }
}
