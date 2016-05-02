using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Teleportable : MonoBehaviour
{

    public Transform Surrogate;

    Transform trans;
    Rigidbody rigid;

    bool useSurrogate;
    Transform activeClone;
    Vector3 clonePositionShift;
    string sideEntered;
    

    void Start()
    {
        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
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
        if (other.name != sideEntered)
        {
            EnterSide(other.name);
            activeClone = MakeClone();
        }
    }
    void OnTriggerStay(Collider other)
    {
        Vector3 clonePosition = trans.position + clonePositionShift * 40;

        Vector3 maskedPosition = trans.position;
        maskedPosition.x *= clonePositionShift.x;
        maskedPosition.y *= clonePositionShift.y;
        maskedPosition.z *= clonePositionShift.z;

        Transform[] originals = trans.GetComponentsInChildren<Transform>();
        Transform[] clones = trans.GetComponentsInChildren<Transform>();
        for (int i = 0; i < originals.Length; i++)
        {
            clones[i].localPosition = originals[i].localPosition;
            clones[i].localRotation = originals[i].localRotation;
        }

        if (maskedPosition.sqrMagnitude>=400)
        {
            trans.position = clonePosition;
            string otherSide = sideEntered.Substring(0, 1);
            if (sideEntered.Contains("+"))
            {
                otherSide += "-";
            }
            else
            {
                otherSide += "+";
            }
            EnterSide(otherSide);
            clonePosition = trans.position + clonePositionShift * 40;
        }
        
        activeClone.position = clonePosition;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name==sideEntered)
        {
            Destroy(activeClone.gameObject);
            sideEntered = "";
        }
    }
    void EnterSide(string side)
    {
        sideEntered = side;
        switch (sideEntered)
        {
            case "Z+":
            case "Z-":
                {
                    clonePositionShift = new Vector3(0, 0, -1);
                }
                break;
            case "Y+":
            case "Y-":
                {
                    clonePositionShift = new Vector3(0, -1, 0);
                }
                break;
            case "X+":
            case "X-":
                {
                    clonePositionShift = new Vector3(-1, 0, 0);
                }
                break;
            default:
                break;
        }
        if (side.Contains("-"))
        {
            clonePositionShift *= -1;
        }
    }
    Transform MakeClone()
    {
        Transform clone = Instantiate<Transform>(trans);
        foreach (MonoBehaviour script in clone.GetComponents<MonoBehaviour>())
        {
            Destroy(script);
        }
        foreach (MonoBehaviour script in clone.GetComponentsInChildren<MonoBehaviour>())
        {
            Destroy(script);
        }
        foreach (Rigidbody rig in clone.GetComponents<Rigidbody>())
        {
            Destroy(rig);
        }
        foreach (Rigidbody rig in clone.GetComponentsInChildren<Rigidbody>())
        {
            Destroy(rig);
        }
        return clone;
    }
}
