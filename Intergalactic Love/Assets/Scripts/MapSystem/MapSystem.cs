using System.Collections;
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

    private void Start()
    {
        itineraryTracer = GetComponent<ItineraryTracer>();
        itineraryTracer.Initialize(this);

        canvas.Initialize(this);

        canvas.SetFuel(0);

        GameManager.gm.player.DisablePlayer();
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
