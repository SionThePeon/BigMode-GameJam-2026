using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class slideShow : MonoBehaviour
{
    public Sprite[] sprites;
    public float intervalSeconds = 2f;
    
    private Image imageComponent;

     void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    public void PlaySlideshowThenLoadScene()
    {
        gameObject.SetActive(true); 
        StartCoroutine(SlideshowCoroutine());
    }

    IEnumerator SlideshowCoroutine()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            imageComponent.sprite = sprites[i];
            yield return new WaitForSeconds(intervalSeconds);
        }
        
        SceneManager.LoadSceneAsync("FullMap");
    }
}