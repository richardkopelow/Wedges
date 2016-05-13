using UnityEngine;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour
{
    public Rigidbody AsteroidPart;
    public List<Rigidbody> Parts;
    public float Attraction;

    Transform trans;

    void Start()
    {
        trans = GetComponent<Transform>();
        Parts = new List<Rigidbody>();
    }

    void FixedUpdate()
    {
        #region Forces
        for (int i = 0; i < Parts.Count; i++)
        {
            Vector3 force = new Vector3();
            for (int j = 0; j < Parts.Count; j++)
            {
                if (i != j)
                {
                    Vector3 diff = Parts[j].position - Parts[i].position;
                    force+=(Attraction * diff.normalized / Mathf.Abs(Mathf.Pow(diff.magnitude, 4)));
                }
            }
            Parts[i].AddForce(force);
            if (force.magnitude < 0.1)
            {
                if (Parts[i].GetComponent<TimeDecay>() == null)
                {
                    TimeDecay td = Parts[i].gameObject.AddComponent<TimeDecay>();
                    td.LiveTime = 2;
                    td.OnDeath += onAsteroidDeath;
                }
            }
            else
            {
                TimeDecay td = Parts[i].GetComponent<TimeDecay>();
                if (td != null)
                {
                    Destroy(td);
                }
            }
        }
        #endregion
    }
    void makeAsteroid(Vector3 position, int size)
    {
        Rigidbody body1 = Instantiate<Rigidbody>(AsteroidPart);
        Parts.Add(body1);
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
                Parts.Add(bodyi);
                bodyi.GetComponent<Transform>().parent = trans;
                bodyi.position = info.point - cast.normalized * 0.6f;
            }
        }
    }
    public void MakeAsteroid()
    {
        Vector3 startingPlace = new Vector3(Random.Range(-1 * Globals.GameRadius, Globals.GameRadius), Random.Range(-1 * Globals.GameRadius, Globals.GameRadius), Random.Range(-1 * Globals.GameRadius, Globals.GameRadius));
        makeAsteroid(startingPlace,Random.Range(2,10));
    }
    void onAsteroidDeath(TimeDecay sender)
    {
        Parts.Remove(sender.GetComponent<Rigidbody>());
        Globals.Score++;
    }
}
