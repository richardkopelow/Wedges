using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HighScores
{
    private static HighScores _instance;
    public static HighScores Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = new HighScores();
            }
            return _instance;
        }
        set { _instance = value; }
    }

    private List<PlayerScore> _scores;
    public List<PlayerScore> Scores
    {
        get { return _scores; }
        private set { _scores = value; }
    }

    private HighScores()
    {
        Scores = new List<PlayerScore>(10);
        for (int i = 0; i < 10; i++)
        {
            Scores.Add(new PlayerScore(PlayerPrefs.GetString(i.ToString(), "Foo Bar"), PlayerPrefs.GetInt(i.ToString(), 0)));
        }
        Scores = Scores.OrderBy(s => s.Score).ToList<PlayerScore>();
    }

    public void SubmitScore(string name, int score)
    {
        Scores.Add(new PlayerScore(name, score));
        Scores = Scores.OrderBy(s => s.Score).Reverse<PlayerScore>().ToList<PlayerScore>();
        Scores.RemoveAt(10);
        save();
    }

    void save()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetString(i.ToString(), Scores[i].Name);
            PlayerPrefs.SetInt(i.ToString(), Scores[i].Score);
        }
    }
}

public class PlayerScore
{
    public string Name { get; set; }
    public int Score { get; set; }

    public PlayerScore(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
