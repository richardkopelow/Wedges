using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Globals
{
    public static float GameRadius = 20;
    public static int RenderDistance = 1;
    public static Transform GameSpace;

    public static T Instantiate<T>(T obj) where T:UnityEngine.Object
    {
        T o = GameObject.Instantiate<T>(obj);
        Transform tra = o is GameObject ? (o as GameObject).GetComponent<Transform>() : (o as Component).GetComponent<Transform>();
        tra.parent = GameSpace;
        return o;
    }
}
