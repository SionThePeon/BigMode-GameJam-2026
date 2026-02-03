using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI pizzaText;
     public RectTransform fuelNeedle;

    public float minRotation = -30f;
    public float maxRotation = 30f;


    void Update()
    {
        pizzaText.text = CarController.pizzaCount.ToString();

        float percent = CarController.gas / CarController.maxGas;
        float rotation = Mathf.Lerp(minRotation, maxRotation, percent);

        fuelNeedle.localRotation = Quaternion.Euler(0, 0, rotation);
    }
}