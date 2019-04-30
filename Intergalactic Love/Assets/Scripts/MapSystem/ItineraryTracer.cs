using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItineraryTracer : MonoBehaviour
{
    public TrailRenderer trail;

    private bool isTracing;

    private MapSystem ms;
    private LayerMask everything;
    private LayerMask plane;
    private Collider player;

    public void Initialize(MapSystem ms)
    {
        this.ms = ms;

        everything = ~0;
        plane = LayerMask.GetMask("UI");
    }

    private float distance;

    private Vector3 lastHit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("CLICK");

            RaycastHit hit;
            if (RayCast(everything, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    player = hit.collider;
                    isTracing = true;
                    distance = 0f;

                    lastHit = hit.point;

                    ms.playerPreview.localPosition = Vector3.zero;

                    trail.Clear();
                }
            }

        }

        if (Input.GetMouseButton(0) && isTracing)
        {
            print("DRAG");
            RaycastHit hit;
            if (RayCast(plane, out hit))
            {
                ms.playerPreview.transform.position = hit.point;

                distance += Vector3.Distance(hit.point, lastHit);
                lastHit = hit.point;
                ms.SetFuelbar(distance);
            }

            if (distance > ms.GetMaxDistance())
            {
                ms.SetFuelbar(ms.GetMaxDistance());
                Stop();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Stop();
        }
    }

    private void Stop()
    {
        print("STOP");
        isTracing = false;
    }

    private bool RayCast(LayerMask mask, out RaycastHit hit)
    {
        Ray ray = ms.mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, 1000, mask);
    }
}


public class Itinerary
{
    public enum Event
    {
        Nothing,
        Asteroids,
        Special,
    }

    public List<Event> events;

    public Action specialEvent;
}