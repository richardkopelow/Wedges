using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public Transform PlayerCam;
    public Transform PartnerPortal;
    public Transform RenderCam;

    //I think that there is a better way to do the portals by changing the angles and such the cameras are looking at based on the player
    //I am not sure what that is
    /*
    Vector3 position;
    float distance;
    // Use this for initialization
    void Start()
    {
        position = GetComponent<Transform>().position;
        distance = RenderCam.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pToPDiff = PlayerCam.position-PartnerPortal.position;

        RenderCam.position = position + pToPDiff.normalized * distance;
        RenderCam.LookAt(position);
    }
    */
}
