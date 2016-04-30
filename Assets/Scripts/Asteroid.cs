using UnityEngine;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour
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

        parts.Add(Instantiate<Rigidbody>(AsteroidPart));
        parts[0].GetComponent<Transform>().parent = trans;
        parts[0].GetComponent<Transform>().localPosition = Vector3.zero;

        for (int i = 1; i < Size; i++)
        {
            Vector3 cast = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1), Random.Range(-1f, 1f)).normalized * 3;
            Ray ray = new Ray(trans.position - cast, cast);
            RaycastHit info;
            if (Physics.Raycast(ray,out info,8))
            {
                parts.Add(Instantiate<Rigidbody>(AsteroidPart));
                parts[i].GetComponent<Transform>().parent = trans;
                parts[i].position = info.point - cast.normalized * 0.6f;
            }
        }
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
}
