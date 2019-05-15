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

    private bool hasItinerary;

    public void Initialize(MapSystem ms)
    {
        this.ms = ms;

        hasItinerary = false;
        ms.canvas.SetGoButtonInteractible(false);

        everything = ~0;
        plane = LayerMask.GetMask("UI");

        ms.canvas.playerTarget.SetActive(true);
        ms.canvas.planet2Target.SetActive(false);
    }

    private float distance;

    private Vector3 lastHit;

    private void ResetItinerary()
    {
        hasItinerary = false;
        ms.canvas.SetGoButtonInteractible(false);
        distance = 0f;


        ms.playerPreview.localPosition = Vector3.zero;

        trail.Clear();
    }

    private void Update()
    {
        if (!ms.canDoAnything) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (RayCast(everything, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    player = hit.collider;
                    lastHit = hit.point;

                    isTracing = true;
                    ResetItinerary();
                }
            }

        }

        if (Input.GetMouseButton(0) && isTracing)
        {
            RaycastHit hit;
            if (RayCast(plane, out hit))
            {
                Vector3 point = hit.point;
                float addedDistance = Vector3.Distance(point, lastHit);

                if (distance + addedDistance > ms.GetMaxDistance())
                {
                    float coef = (ms.GetMaxDistance() - distance) / addedDistance;
                    point = Vector3.Lerp(lastHit, point, coef);

                    distance = ms.GetMaxDistance();
                    Stop();
                }
                else
                {
                    distance += addedDistance;
                }

                ms.playerPreview.transform.position = point;
                ms.SetFuelbar(distance);

                ms.canvas.playerTarget.SetActive(false);
                ms.canvas.planet2Target.SetActive(true);

                lastHit = point;
            }

            
        }

        if (Input.GetMouseButtonUp(0) && isTracing)
        {
            Stop();
        }
    }

    private void Stop()
    {
        isTracing = false;
        hasItinerary = IsItineraryDestination(Itinerary.MapPoint.Event.Planet2);
        ms.canvas.SetGoButtonInteractible(hasItinerary);

        if (!hasItinerary)
        {
            ResetItinerary();
            StartCoroutine(ShowDumbassTip());
        }
    }

    IEnumerator ShowDumbassTip()
    {
        ms.canDoAnything = false;
        yield return StartCoroutine(ms.canvas.ShowDumbassTip());
        ms.canDoAnything = true;
    }



    private bool RayCast(LayerMask mask, out RaycastHit hit)
    {
        Ray ray = ms.mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, 1000, mask);
    }

    public Itinerary GetItinerary(Itinerary.MapPoint.Event destination)
    {
        Itinerary result = new Itinerary();

        Vector3[] positions = new Vector3[trail.positionCount];
        trail.GetPositions(positions);

        result.points = new List<Itinerary.MapPoint>();

        foreach (Vector3 pos in positions)
        {
            Collider[] cols = Physics.OverlapBox(pos, new Vector3(0.01f, 1, 0.01f));

            Itinerary.MapPoint p = new Itinerary.MapPoint();

            p.position = pos;

            p.mapEvent = Itinerary.MapPoint.Event.Nothing;

            foreach (Collider col in cols)
            {
                if (col.CompareTag("InterestPoint"))
                {
                    p.mapEvent = col.GetComponent<InterestPoint>().Event;
                    break;
                }
            }

            result.points.Add(p);

            if (p.mapEvent == destination)
                break;
        }

        return result;
    }

    public bool IsItineraryDestination(Itinerary.MapPoint.Event mapEvent)
    {
        GameManager.gm.currentItinerary = GetItinerary(mapEvent);
        return GameManager.gm.currentItinerary.points[GameManager.gm.currentItinerary.points.Count - 1].mapEvent == mapEvent;
    }
}

[System.Serializable]
public class Itinerary
{
    [System.Serializable]
    public class MapPoint
    {
        public Vector3 position;
        public Event mapEvent;
        
        public enum Event
        {
            Nothing,
            Asteroids,
            Planet2,
        }
    }

    public List<MapPoint> points;
}