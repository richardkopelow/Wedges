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
            Vector3 force = new Vector3();
            for (int j = 0; j < parts.Count; j++)
            {
                if (i != j)
                {
                    Vector3 diff = parts[j].position - parts[i].position;
                    force+=(Attraction * diff.normalized / Mathf.Abs(Mathf.Pow(diff.magnitude, 4)));
                }
            }
            parts[i].AddForce(force);
            if (force.magnitude < 0.1)
            {
                if (parts[i].GetComponent<TimeDecay>() == null)
                {
                    TimeDecay td = parts[i].gameObject.AddComponent<TimeDecay>();
                    td.LiveTime = 2;
                    td.OnDeath += onAsteroidDeath;
                }
            }
            else
            {
                TimeDecay td = parts[i].GetComponent<TimeDecay>();
                if (td != null)
                {
                    Destroy(td);
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
    void onAsteroidDeath(TimeDecay sender)
    {
        parts.Remove(sender.GetComponent<Rigidbody>());
        Globals.Score++;
    }
}
