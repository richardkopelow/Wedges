using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    Transform trans;
    Rigidbody rigid;
    void Start()
    {
        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (rigid.velocity.magnitude<3)
        {
            Vector3 force = trans.forward * Input.GetAxis("Vertical") + trans.right * Input.GetAxis("Horizontal");
            rigid.AddForce(force);
        }
    }
}
