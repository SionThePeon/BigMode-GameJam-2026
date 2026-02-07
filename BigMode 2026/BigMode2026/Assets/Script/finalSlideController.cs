using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class finalSlideController : MonoBehaviour
{
    public Sprite[] sprites;
    public float intervalSeconds = 2f;
    public Button quitButton; 
    
    public Image imageComponent;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        
        
        if (quitButton != null)
        {
            quitButton.gameObject.SetActive(false);
        }
        
        StartCoroutine(SlideshowCoroutine());
    }

    IEnumerator SlideshowCoroutine()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            imageComponent.sprite = sprites[i];
            
            
            if (i == sprites.Length - 1)
            {
                
                if (quitButton != null)
                {
                    quitButton.gameObject.SetActive(true);
                }
                yield break;
            }
            
            yield return new WaitForSeconds(intervalSeconds);
        }
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}
