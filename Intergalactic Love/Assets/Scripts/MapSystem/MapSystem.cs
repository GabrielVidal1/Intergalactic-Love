using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public Camera mainCamera;

    public Transform playerPreview;

    private ItineraryTracer itineraryTracer;

    public MSCanvas canvas;

    [SerializeField] private float maxDistance;
    public float GetMaxDistance()
    { return maxDistance; }

    private void Start()
    {
        itineraryTracer = GetComponent<ItineraryTracer>();
        itineraryTracer.Initialize(this);

        canvas.SetFuel(0);
    }

    public void SetFuelbar(float distance)
    {
        canvas.SetFuel(distance / maxDistance);
    }

}
