using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    public GameObject slideshowImage;
    public GameObject buttonPanel;
    
    private slideShow slideshowController;

    void Start()
    {
        slideshowController = slideshowImage.GetComponent<slideShow>();
        slideshowImage.SetActive(false); 
    }

    public void OnPlayGame()
    {
        if (buttonPanel != null)
        {
            buttonPanel.SetActive(false);
        }
        
        slideshowController.PlaySlideshowThenLoadScene();
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}