using UnityEngine;
using System.Collections;

public class TimeDecay : MonoBehaviour
{
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
        if (liveTime<0)
        {
            Destroy(gameObject);
            Debug.Log("Die");
        }
    }
}
