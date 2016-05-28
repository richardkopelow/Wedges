using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour
{
    public Text ScoreLabel;
    public Text HealthLabel;
    public Ship Player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ScoreLabel.text = "Score: " + Globals.Score;
        HealthLabel.text = "Health: " + Player.Health;
    }
}
