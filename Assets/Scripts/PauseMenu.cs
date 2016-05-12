using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Holder;

    void Start()
    {

    }
    
    void Update()
    {

    }

    public void Show()
    {
        Holder.SetActive(true);
    }

    public void Hide()
    {
        Holder.SetActive(false);
    }
    public void OnExit()
    {
        Globals.UnPause();
        SceneManager.LoadScene(0);
    }
    public void OnContinue()
    {
        Hide();
        Globals.UnPause();
    }
}
