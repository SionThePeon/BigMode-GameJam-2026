using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public CarController playerStats;

    public TextMeshProUGUI pizzaText;
     public RectTransform fuelNeedle;

    public float minRotation = -30f;
    public float maxRotation = 30f;


    void Update()
    {
        pizzaText.text = "Pizza: " + playerStats.pizzaCount;

        
        float percent = playerStats.gas / playerStats.maxGas;
        float rotation = Mathf.Lerp(minRotation, maxRotation, percent);

        fuelNeedle.localRotation = Quaternion.Euler(0, 0, rotation);
    }
}