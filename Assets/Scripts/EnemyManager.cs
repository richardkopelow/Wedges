using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public Rigidbody AsteroidPart;

    Transform trans;
    AsteroidManager asteroids;

    float elapsedTime;
    float lastAsteroidSpawn;

    void Start()
    {
        trans = GetComponent<Transform>();

        GameObject asteroidsGO = new GameObject();
        Transform asteroidsTrans = asteroidsGO.GetComponent<Transform>();
        asteroidsTrans.parent = trans;
        asteroids = asteroidsGO.AddComponent<AsteroidManager>();
        asteroids.Attraction = 15;
        asteroids.AsteroidPart = AsteroidPart;
        elapsedTime = 0;
    }
    
    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;

        {
            if (asteroids.Parts.Count < 30)
            {
                if ((elapsedTime - lastAsteroidSpawn) * elapsedTime > 15)
                {
                    asteroids.MakeAsteroid();
                    lastAsteroidSpawn = elapsedTime;
                }
            }
        }
    }
}
