using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class HighScoresMenu : MonoBehaviour
{
    public Text ScoreReadout;

    void Start()
    {
        setText();
    }

    public void OnClearClicked()
    {
        HighScores.Instance.Clear();
        setText();
    }

    void setText()
    {
        StringBuilder sb = new StringBuilder("High Scores\n");
        foreach (PlayerScore ps in HighScores.Instance.Scores)
        {
            sb.AppendFormat("{0}\t{1}\n", ps.Name, ps.Score);
        }
        sb.Remove(sb.Length - 1, 1);
        ScoreReadout.text = sb.ToString();
    }

}
