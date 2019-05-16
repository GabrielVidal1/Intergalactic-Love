﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public Camera mainCamera;

    public Transform playerPreview;

    public ItineraryTracer itineraryTracer;

    public MSCanvas canvas;

    [SerializeField] private float maxDistance;
    public float GetMaxDistance()
    { return maxDistance; }

    public bool canDoAnything;

    private void Start()
    {
        StartCoroutine(StartCoroutine());
    }

    IEnumerator StartCoroutine()
    {
        canDoAnything = false;

        itineraryTracer = GetComponent<ItineraryTracer>();
        itineraryTracer.Initialize(this);

        canvas.Initialize(this);
        canvas.SetFuel(0);

        if (GameManager.gm.player != null)
            GameManager.gm.player.DisablePlayer();

        yield return StartCoroutine(canvas.ShowTips());

        canDoAnything = true;
    }

    public void SetFuelbar(float distance)
    {
        canvas.SetFuel(distance / maxDistance);
    }

    public enum SceneName
    {
        SpacePhase,
        Planet
    }

    public void ChangeScene(SceneName t)
    {
        switch (t)
        {
            case SceneName.SpacePhase:
                SceneManager.LoadScene("Spacephase");
                break;
            case SceneName.Planet:
                SceneManager.LoadScene("Planet1"); ///TO CHANGE
                break;
        }
    }

}
