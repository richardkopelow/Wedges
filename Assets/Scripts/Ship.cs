using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    Transform trans;
    Rigidbody rigid;

    Vector3 lastMousePos;

    void Start()
    {
        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        lastMousePos = Input.mousePosition;
    }
    void Update()
    {
        #region NonVRCamControl

        trans.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
        trans.Rotate(Vector3.left, Input.GetAxis("Mouse Y"));
        trans.localEulerAngles = new Vector3(trans.localEulerAngles.x, trans.localEulerAngles.y, 0);
        #endregion
    }
    void FixedUpdate()
    {
        Vector3 force = 3 * (trans.forward * Input.GetAxis("Vertical") + trans.right * Input.GetAxis("Horizontal"));
        if (rigid.velocity.magnitude < 4)
        {
            if (force.x * rigid.velocity.x > 0)
            {
                force.x = 0;
            }
            if (force.y * rigid.velocity.y > 0)
            {
                force.y = 0;
            }
        }
        rigid.AddForce(force);
    }
}
