using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject Holder;

    public void Show()
    {
        Holder.SetActive(true);
        FindObjectOfType<EventSystem>().SetSelectedGameObject(GameObject.Find("ContinueButton"));
    }

    public void Hide()
    {
        Holder.SetActive(false);
    }
    public void OnExitClicked()
    {
        Globals.UnPause();
        SceneManager.LoadScene(0);
    }
    public void OnContinueClicked()
    {
        Hide();
        Globals.UnPause();
    }
}
