using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Globals
{
    public static float GameRadius = 20;
#if UNITY_EDITOR || UNITY_WEBGL
    public static int RenderDistance = 1;
#else
    public static int RenderDistance = 2;
#endif
    public static Transform GameSpaceTrans;
    public static GameSpace GSpace;
    public static int Score;
    public static bool Paused;


    public static T Instantiate<T>(T obj) where T:UnityEngine.Object
    {
        T o = GameObject.Instantiate<T>(obj);
        Transform tra = o is GameObject ? (o as GameObject).GetComponent<Transform>() : (o as Component).GetComponent<Transform>();
        tra.parent = GameSpaceTrans;
        return o;
    }
    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
    }
    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public static void Pause()
    {
        Paused = true;
        UnlockCursor();
        Time.timeScale = 0;
    }
    public static void UnPause()
    {
        Paused = false;
        Time.timeScale = 1;
        LockCursor();
    }
}
