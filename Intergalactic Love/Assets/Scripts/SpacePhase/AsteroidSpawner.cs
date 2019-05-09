using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public AsteroidInfo[] asteroidInfos;

    [System.Serializable]
    public class AsteroidInfo
    {
        public Asteroid prefab;
        public AnimationCurve sizeDistrib;
        public float maxSize;
    }

    public float dist = 100;

    [Range(0f, 1f)]
    [SerializeField] private float distRadiusRatio;

    [Range(1, 5)]
    [SerializeField] private int asteroidPerFrame = 1;

    [SerializeField] private int maxAsteroids;

    private float radius;
    private SpacePhaseManager spacePhaseManager;

    [SerializeField] private List<Asteroid> asteroids;

    private Transform parent;

    public bool shouldSpawnAsteroids = false;

    [SerializeField]
    private float lastZ;
    private bool isClear;

    private int index;

    public void Initialize(SpacePhaseManager spacePhaseManager)
    {
        this.spacePhaseManager = spacePhaseManager;
        radius = dist * distRadiusRatio;

        asteroids = new List<Asteroid>();

        StartCoroutine(StartSpawning());

        parent = (new GameObject("Asteroid Parent")).transform;
    }

    IEnumerator StartSpawning()
    {
        while (true)
        {
            while (!shouldSpawnAsteroids)
            {
                if (!isClear && spacePhaseManager.player.transform.position.z > lastZ + 20)
                {
                    for (int i = 0; i < asteroids.Count; i++)
                    {
                        if (asteroids[i] != null)
                            Destroy(asteroids[i].gameObject);
                    }
                    asteroids.Clear();
                    isClear = true;
                }
                yield return 0;
            }
            for (int k = 0; k < asteroidPerFrame; k++)
            {
                if (asteroids.Count > maxAsteroids - 1)
                {
                    Destroy(asteroids[0].gameObject);
                    asteroids.RemoveAt(0);
                }

                Vector2 p = Random.insideUnitCircle;
                Quaternion r = Quaternion.Euler(Random.insideUnitSphere * 180f);

                Vector3 pos = new Vector3(p.x * radius,
                    p.y * 30,
                    dist) + spacePhaseManager.player.transform.position;

                Asteroid a = Instantiate(asteroidInfos[index].prefab,
                    pos , r, parent);

                a.Init(asteroidInfos[index].maxSize *
                    asteroidInfos[index].sizeDistrib.Evaluate(Random.value));

                index = (index + 1) % asteroidInfos.Length;

                asteroids.Add(a);

                lastZ = dist + spacePhaseManager.player.transform.position.z;
                isClear = false;

            }
            yield return 0;
        }
    }

}
