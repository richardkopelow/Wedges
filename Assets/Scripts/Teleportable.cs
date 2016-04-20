using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Teleportable : MonoBehaviour
{

    public Transform Surrogate;

    Transform trans;

    bool useSurrogate;
    Transform activeClone;
    Vector3 clonePositionShift;
    string sideEntered;

    void Start()
    {
        trans = GetComponent<Transform>();
        useSurrogate = Surrogate != null;
    }
    void Update()
    {
        /*
        if (activeClone!=null)
        {
            List<Transform> originalTransforms = new List<Transform>(trans.GetComponentsInChildren<Transform>());
            originalTransforms.Add(trans);
            List<Transform> cloneTransforms = new List<Transform>(trans.GetComponentsInChildren<Transform>());
            cloneTransforms.Add(trans);
            int i;
            for (i = 0; i < originalTransforms.Count-1; i++)
            {
                cloneTransforms[i].localPosition = originalTransforms[i].localPosition;
            }
            cloneTransforms[i].localPosition = originalTransforms[i].localPosition+clonePositionShift;
        }
        */
    }
    void OnTriggerEnter(Collider other)
    {
        sideEntered = other.name;
        switch (sideEntered)
        {
            case "Z+":
            case "Z-":
                {
                    trans.position = new Vector3(trans.position.x, trans.position.y, trans.position.z * -1);
                    Vector3 toCenter = (Vector3.zero - trans.position).normalized * 0.1f;
                    trans.position = trans.position + toCenter;
                }
                break;
            case "Y+":
            case "Y-":
                {
                    trans.position = new Vector3(trans.position.x, trans.position.y * -1, trans.position.z);
                    Vector3 toCenter = (Vector3.zero - trans.position).normalized * 0.1f;
                    trans.position = trans.position + toCenter;
                }
                break;
            case "X+":
            case "X-":
                {
                    trans.position = new Vector3(trans.position.x * -1, trans.position.y, trans.position.z);
                    Vector3 toCenter = (Vector3.zero - trans.position).normalized * 0.1f;
                    trans.position = trans.position + toCenter;
                }
                break;
            default:
                break;
        }
    }
    void OnTriggerExit(Collider other)
    {
        
    }
    Transform MakeClone()
    {
        Transform clone = Instantiate<Transform>(trans);
        foreach (MonoBehaviour script in clone.GetComponents<MonoBehaviour>())
        {
            script.enabled = false;
        }
        foreach (MonoBehaviour script in clone.GetComponentsInChildren<MonoBehaviour>())
        {
            script.enabled = false;
        }
        return clone;
    }
}
