using UnityEngine;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour
{
    public Rigidbody AsteroidPart;
    public int Size;
    public float Attraction;

    Transform trans;
    List<Rigidbody> parts;

    void Start()
    {
        trans = GetComponent<Transform>();
        parts = new List<Rigidbody>(Size);

        makeAsteroid(trans.position, Size);
        makeAsteroid(new Vector3(10,10,0), Size);
        makeAsteroid(new Vector3(-10, -10, 0), Size);
    }

    void Update()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            for (int j = 0; j < parts.Count; j++)
            {
                if (i != j)
                {
                    Vector3 diff = parts[j].position - parts[i].position;
                    parts[i].AddForce(Attraction * diff.normalized / Mathf.Abs(Mathf.Pow(diff.magnitude, 3)));
                }
            }
        }
    }
    void makeAsteroid(Vector3 position, int size)
    {

        Rigidbody body1 = Instantiate<Rigidbody>(AsteroidPart);
        parts.Add(body1);
        body1.GetComponent<Transform>().parent = trans;
        body1.GetComponent<Transform>().position = position;

        for (int i = 1; i < size; i++)
        {
            Vector3 cast = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1), Random.Range(-1f, 1f)).normalized * 3;
            Ray ray = new Ray(position - cast, cast);
            RaycastHit info;
            if (Physics.Raycast(ray, out info, 8))
            {
                Rigidbody bodyi = Instantiate<Rigidbody>(AsteroidPart);
                parts.Add(bodyi);
                bodyi.GetComponent<Transform>().parent = trans;
                bodyi.position = info.point - cast.normalized * 0.6f;
            }
        }
    }
}
