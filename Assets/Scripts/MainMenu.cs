using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public void OnPlayClicked()
    {
        Globals.Score = 0;
        SceneManager.LoadScene("PlayScene");
    }
    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
