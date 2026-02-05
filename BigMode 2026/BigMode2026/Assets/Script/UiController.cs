using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI pizzaText;
     public RectTransform fuelNeedle;

     public TextMeshProUGUI moneyText;

    public float minRotation = -30f;
    public float maxRotation = 30f;


    public TextMeshProUGUI tipPopupText; 
    private CanvasGroup tipCanvasGroup;

    void Start()
    {
        tipCanvasGroup = tipPopupText.GetComponent<CanvasGroup>();
        if (tipCanvasGroup == null)
        {
            tipCanvasGroup = tipPopupText.gameObject.AddComponent<CanvasGroup>();
        }
        tipPopupText.gameObject.SetActive(false);
    }

    void Update()
    {
        pizzaText.text = CarController.pizzaCount.ToString();
        moneyText.text = "$" + CarController.money.ToString();

        float percent = CarController.gas / CarController.maxGas;
        float rotation = Mathf.Lerp(minRotation, maxRotation, percent);

        fuelNeedle.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    public void ShowTipPopup(int tipAmount)
    {
        tipPopupText.text = "$" + tipAmount; 
        tipPopupText.gameObject.SetActive(true);
        tipCanvasGroup.alpha = 1f; 
        StartCoroutine(FadeOutPopup(2f)); 
    }
    
    //Had to look up how to add fade effect
    IEnumerator FadeOutPopup(float duration)
    {
        yield return new WaitForSeconds(duration); 
        
        float fadeTime = 0.5f; // How long the fade takes
        float elapsed = 0f;
        
        while (elapsed < fadeTime) // Gradually reduce opacity
        {
            elapsed += Time.deltaTime;
            tipCanvasGroup.alpha = 1f - (elapsed / fadeTime); // Calculate transparency
            yield return null;
        }
        
        tipPopupText.gameObject.SetActive(false); 
    }

}