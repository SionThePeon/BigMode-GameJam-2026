using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class startMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     public void playGame()
    {
        SceneManager.LoadSceneAsync("FullMap");
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
