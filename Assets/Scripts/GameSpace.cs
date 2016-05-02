using UnityEngine;
using System.Collections.Generic;

public class GameSpace : MonoBehaviour
{
    Transform trans;

    List<Transform> clones;
    // Use this for initialization
    void Start()
    {
        trans = GetComponent<Transform>();
        Globals.GameSpace = trans;

        clones = new List<Transform>();
        buildBoard();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform clone in clones)
        {
            Transform[] originals = trans.GetComponentsInChildren<Transform>();
            Transform[] cloneChildren = clone.GetComponentsInChildren<Transform>();
            if (cloneChildren.Length!=originals.Length)
            {
                buildBoard();
                return;
            }
            
            for (int i = 1; i < originals.Length; i++)
            {
                cloneChildren[i].localPosition = originals[i].localPosition;
                cloneChildren[i].localRotation = originals[i].localRotation;
            }
        }
    }
    void buildBoard()
    {
        foreach (Transform clone in clones)
        {
            DestroyImmediate(clone.gameObject);
        }
        clones = new List<Transform>();

        for (int x = Globals.RenderDistance*-1; x <= Globals.RenderDistance; x++)
        {
            for (int y = Globals.RenderDistance * -1; y <= Globals.RenderDistance; y++)
            {
                for (int z = Globals.RenderDistance * -1; z <= Globals.RenderDistance; z++)
                {
                    if (x + y + z != 0)
                    {
                        makeClone(Globals.GameRadius * 2 * new Vector3(x, y, z));
                    }
                }
            }
        }
    }
    void makeClone(Vector3 offset)
    {
        Transform clone = Instantiate<Transform>(trans);
        clone.position = offset;
        foreach (MonoBehaviour script in clone.GetComponentsInChildren<MonoBehaviour>())
        {
            Destroy(script);
        }
        foreach (Rigidbody rig in clone.GetComponentsInChildren<Rigidbody>())
        {
            Destroy(rig);
        }
        foreach (Collider coll in clone.GetComponentsInChildren<Collider>())
        {
            Destroy(coll);
        }
        foreach (Camera cam in clone.GetComponentsInChildren<Camera>())
        {
            GameObject go = cam.gameObject;
            Destroy(go.GetComponent<AudioListener>());
            Destroy(go.GetComponent<GUILayer>());
            Destroy(go.GetComponent<FlareLayer>());
            Destroy(cam);
        }
        clones.Add(clone);
    }
}
