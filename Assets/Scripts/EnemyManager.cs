using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public Rigidbody AsteroidPart;
    public GameObject UFO;

    Transform trans;
    AsteroidManager asteroids;
    
    float lastAsteroidSpawn;
    float ufoNextSpawn;

    void Awake()
    {
        trans = GetComponent<Transform>();
        GameObject asteroidsGO = new GameObject();
        Transform asteroidsTrans = asteroidsGO.GetComponent<Transform>();
        asteroidsTrans.parent = trans;
        asteroids = asteroidsGO.AddComponent<AsteroidManager>();
        asteroids.Attraction = 15;
        asteroids.AsteroidPart = AsteroidPart;
    }
    
    void FixedUpdate()
    {
        {
            if (asteroids.Parts.Count < 30)
            {
                if ((Time.fixedTime - lastAsteroidSpawn) * Time.fixedTime > 15)
                {
                    asteroids.MakeAsteroid();
                    lastAsteroidSpawn = Time.fixedTime;
                }
            }
        }
        if (Time.fixedTime>70&&Time.fixedTime>ufoNextSpawn)
        {
            GameObject ufoGo=Instantiate(UFO);
            ufoGo.GetComponent<Transform>().parent = trans;
            ufoNextSpawn = Time.fixedTime + Random.Range(6, 15);
        }
    }
}
