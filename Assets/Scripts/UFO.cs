using UnityEngine;
using System.Collections;

public class UFO : MonoBehaviour
{
    public GameObject Wedge;
    Transform trans;
    Rigidbody rigid;

    void Start()
    {
        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        rigid.position = Random.insideUnitSphere * Globals.GameRadius;
        rigid.velocity = Random.insideUnitSphere * 3;

        StartCoroutine(firingLogic());
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Globals.Score += 15;
    }

    IEnumerator firingLogic()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            GameObject wedge = Instantiate(Wedge);
            Rigidbody wedgeRigid = wedge.GetComponent<Rigidbody>();

            Vector3 pos = (Globals.Player.position - rigid.position).normalized * 2;
            wedgeRigid.position = rigid.position + pos;
            wedgeRigid.velocity = pos;
            Transform wedgeTrans = wedge.GetComponent<Transform>();
            wedgeTrans = trans.parent;
            //wedgeTrans.LookAt(Globals.Player.position);
            //wedgeTrans.Rotate(new Vector3(-90, 0, 0), Space.Self);

        }
    }
}
