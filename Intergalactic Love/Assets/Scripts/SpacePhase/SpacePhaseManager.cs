using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpacePhaseManager : MonoBehaviour
{
    public SPHealth player;
    public Camera mainCamera;

    [SerializeField] private PlanetPreview planet2Preview;

    [SerializeField] private float totalDuration = 60f;

    private AsteroidSpawner asteroidSpawner;

    private Itinerary itinerary;

    [SerializeField] private Minimap minimap;

    public Slider healthBar;

    [SerializeField] private CanvasGroup fondu;

    void Start()
    {
        GameManager.gm.player.DisablePlayer();
            
        GameManager.gm.spacePhaseManager = this;

        player.spacePhaseManager = this;

        asteroidSpawner = GetComponent<AsteroidSpawner>();
        asteroidSpawner.Initialize(this);

        itinerary = GameManager.gm.currentItinerary;

        StartCoroutine(ExecuteItinerary());
    }

    IEnumerator ExecuteItinerary()
    {
        print("ExecuteItinerary");

        minimap.SetUp(itinerary);

        float stepDuration = totalDuration / itinerary.points.Count;

        for (int i = 0; i < itinerary.points.Count - 1; i++)
        {
            minimap.SetPosition(itinerary, i, stepDuration);
            if (i < itinerary.points.Count - 3)
            {
                asteroidSpawner.shouldSpawnAsteroids = itinerary.points[i + 3].mapEvent == Itinerary.MapPoint.Event.Asteroids;

                switch (itinerary.points[i+3].mapEvent)
                {
                    case Itinerary.MapPoint.Event.Planet2:
                        AddPlanet(planet2Preview.gameObject);
                        break;
                    case Itinerary.MapPoint.Event.Planet3:
                        break;
                    case Itinerary.MapPoint.Event.Planet4:
                        break;
                    default:
                        break;
                }
            }

            switch (itinerary.points[i].mapEvent)
            {
                case Itinerary.MapPoint.Event.Planet2:
                    StartCoroutine(MoveToPlanet("Planet2"));
                    yield break;
                case Itinerary.MapPoint.Event.Planet3:
                    break;
                case Itinerary.MapPoint.Event.Planet4:
                    break;
                default:
                    break;
            }

            yield return new WaitForSecondsRealtime(stepDuration);
        }
    }

    private void AddPlanet(GameObject planet)
    {
        Instantiate(planet, new Vector3(0, 0, asteroidSpawner.dist), Quaternion.identity);
    }

    private IEnumerator MoveToPlanet(string sceneName)
    {
        for (float i = 0f; i < 1f; i+= Time.deltaTime)
        {
            fondu.alpha = i;
            yield return 0; 
        }
        fondu.alpha = 1f;

        SceneManager.LoadScene(sceneName);
    }

    public void SetAsteroidSpawning(bool enable)
    {
        asteroidSpawner.shouldSpawnAsteroids = enable;
    }

    public void Die()
    {

    }

}
