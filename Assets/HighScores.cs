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
        load();
    }

    public void SubmitScore(string name, int score)
    {
        Scores.Add(new PlayerScore(name, score));
        Scores = Scores.OrderBy(s => s.Score).Reverse<PlayerScore>().ToList<PlayerScore>();
        Scores.RemoveAt(10);
        save();
    }

    public void Clear()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetString(i.ToString(), "Foo Bar");
            PlayerPrefs.SetInt(i.ToString(), 0);
        }
        load();
    }

    void save()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetString(i.ToString()+"name", Scores[i].Name);
            PlayerPrefs.SetInt(i.ToString(), Scores[i].Score);
        }
    }
    void load()
    {
        Scores = new List<PlayerScore>(10);
        for (int i = 0; i < 10; i++)
        {
            Scores.Add(new PlayerScore(PlayerPrefs.GetString(i.ToString()+"name", "Foo Bar"), PlayerPrefs.GetInt(i.ToString(), 0)));
        }
        Scores = Scores.OrderBy(s => s.Score).Reverse<PlayerScore>().ToList<PlayerScore>();
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
