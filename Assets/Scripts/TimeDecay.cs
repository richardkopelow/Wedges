using UnityEngine;
using System.Collections;

public class TimeDecay : MonoBehaviour
{
    public delegate void DefaultDelegate(TimeDecay sender);
    public event DefaultDelegate OnDeath;

    public float LiveTime;
    float liveTime;
    // Use this for initialization
    void Start()
    {
        liveTime = LiveTime;
    }

    // Update is called once per frame
    void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime < 0)
        {
            if (OnDeath!=null)
            {
                OnDeath(this);
            }
            DestroyImmediate(gameObject);
        }
    }
}
