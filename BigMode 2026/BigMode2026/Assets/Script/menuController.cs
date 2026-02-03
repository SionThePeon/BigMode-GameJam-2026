using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void playGame()
    {
        SceneManager.LoadSceneAsync("FullMap");
    }

    public void startRun()
    {
        SceneManager.LoadSceneAsync("FullMap");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
