using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Text;
using UnityEngine.EventSystems;

public class DeathMenu : MonoBehaviour
{
    public GameObject Holder;
    public Text Title;
    public InputField NameSpace;
    public Text ScoreReadout;

    public void Show()
    {
        bool isHighScore = false;
        StringBuilder sb = new StringBuilder("High Scores\n");
        foreach (PlayerScore ps in HighScores.Instance.Scores)
        {
            sb.AppendFormat("{0}\t{1}\n", ps.Name, ps.Score);
            
            if (Globals.Score>ps.Score)
            {
                isHighScore = true;
            }
        }
        sb.Remove(sb.Length - 1, 1);
        ScoreReadout.text = sb.ToString();
        if (isHighScore)
        {
            Title.text = "New High Score: "+Globals.Score;
            NameSpace.text = "";
            NameSpace.gameObject.SetActive(true);
        }
        else
        {
            NameSpace.gameObject.SetActive(false);
            Title.text = "Score: " + Globals.Score;
            NameSpace.text = "";
        }

        Holder.SetActive(true);
        FindObjectOfType<EventSystem>().SetSelectedGameObject(GameObject.Find("PlayAgainButton"));
    }

    public void OnPlayAgainClicked()
    {
        HighScores.Instance.SubmitScore(NameSpace.text, Globals.Score);
        Globals.Score = 0;
        SceneManager.LoadScene("PlayScene");
    }

    public void OnExitClicked()
    {
        HighScores.Instance.SubmitScore(NameSpace.text, Globals.Score);
        SceneManager.LoadScene("MainMenu");
    }
}
