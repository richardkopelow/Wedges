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
            
            for (int i = 0; i < originals.Length; i++)
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
        foreach (Camera cam in clone.GetComponentsInChildren<Camera>())
        {
            cam.gameObject.SetActive(false);
        }
        clones.Add(clone);
    }
}
