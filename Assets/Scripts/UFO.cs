using UnityEngine;
using System.Collections;

public class UFO : MonoBehaviour
{
    public GameObject Wedge;
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.position = Random.insideUnitSphere * Globals.GameRadius;
        rigid.velocity = Random.insideUnitSphere * 3;
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Globals.Score += 15;
    }
}
