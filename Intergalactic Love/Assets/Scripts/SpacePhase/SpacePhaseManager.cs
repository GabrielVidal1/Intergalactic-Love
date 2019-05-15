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


    [SerializeField] private GameObject[] spacePhaseTips;
    [SerializeField] private GameObject leftClickIcon;
    [SerializeField] private GameObject leftClickIconText;

    public GameObject explosionPrefab;


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
        for (float i = 0f; i < 1f; i += Time.deltaTime)
        {
            fondu.alpha = 1f-i;
            yield return 0;
        }
        fondu.alpha = 0f;

        if (GameManager.gm.shouldDisplayTips)
        {
            foreach (GameObject tip in spacePhaseTips)
            {
                tip.SetActive(true);
                yield return new WaitForSeconds(2f);

                leftClickIcon.SetActive(true);

                while (Input.GetMouseButtonDown(0))
                    yield return 0;
                while (!Input.GetMouseButtonDown(0))
                    yield return 0;

                leftClickIcon.SetActive(false);
                tip.SetActive(false);
                leftClickIconText.SetActive(false);
            }
        }

        player.GetComponent<SPMovement>().moving = true;

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
                        AddPlanet(planet2Preview);
                        break;
                }
            }

            yield return new WaitForSecondsRealtime(stepDuration);
        }

        switch (itinerary.points[itinerary.points.Count - 1].mapEvent)
        {
            case Itinerary.MapPoint.Event.Planet2:
                StartCoroutine(MoveToPlanet("Planet2"));
                yield break;
        }
    }

    private void AddPlanet(PlanetPreview planet)
    {
        print("Add Planet " + planet.name);

        Vector3 pos = player.transform.position;
        pos.z += asteroidSpawner.dist * 3f;

        PlanetPreview p = Instantiate(planet, pos, Quaternion.identity);
        p.Init(1f);
    }

    private IEnumerator MoveToPlanet(string sceneName)
    {
        print("Move to Planet " + sceneName);

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
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        healthBar.value = 0f;

        for (int i = 0; i < 5; i++)
        {
            GameObject e = Instantiate(explosionPrefab, player.transform.position
                 + Random.insideUnitSphere * 2f, Quaternion.identity);
            e.transform.localScale *= 7f;

            yield return new WaitForSeconds(0.5f);
        }

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        for (float i = 0f; i < 1f; i += Time.deltaTime)
        {
            fondu.alpha = i;
            yield return 0;
        }
        fondu.alpha = 1f;

        GameManager.gm.shouldDisplayTips = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
