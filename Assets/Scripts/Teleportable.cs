﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Teleportable : MonoBehaviour
{

    public Transform Surrogate;

    Transform trans;
    Rigidbody rigid;

    bool useSurrogate;
    

    void Start()
    {
        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        useSurrogate = Surrogate != null;
    }
    void Update()
    {
        bool ported = false;
        Vector3 position = trans.position;
        Transform tToUse = useSurrogate ? Surrogate : trans;
        if (tToUse.position.x>Globals.GameRadius)
        {
            position.x -= 2f*Globals.GameRadius;
            ported = true;
        }
        if (tToUse.position.x < -1*Globals.GameRadius)
        {
            position.x += 2f * Globals.GameRadius;
            ported = true;
        }
        if (tToUse.position.y > Globals.GameRadius)
        {
            position.y -= 2f * Globals.GameRadius;
            ported = true;
        }
        if (tToUse.position.y < -1 * Globals.GameRadius)
        {
            position.y += 2f * Globals.GameRadius;
            ported = true;
        }
        if (tToUse.position.z > Globals.GameRadius)
        {
            position.z -= 2f * Globals.GameRadius;
            ported = true;
        }
        if (tToUse.position.z < -1 * Globals.GameRadius)
        {
            position.z += 2f * Globals.GameRadius;
            ported = true;
        }
        trans.position = position;
        if (ported)
        {
            Globals.GSpace.UpdateClones();
        }
    }
}
