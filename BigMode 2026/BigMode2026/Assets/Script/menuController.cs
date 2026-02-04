using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class menuController : MonoBehaviour
{
    [SerializeField] Image gasUpgradeImage;
    [SerializeField] Image pizzaUpgradeImage;
    [SerializeField] Image tireUpgradeImage;
    [SerializeField] Image pizzaVelUpgradeImage;

    [SerializeField] Sprite[] gasBarSprites;      
    [SerializeField] Sprite[] pizzaBarSprites;    
    [SerializeField] Sprite[] tireBarSprites;     
    [SerializeField] Sprite[] pizzaVelBarSprites; 
    public static int gasLevel = 0;
    public static int pizzaLevel = 0;
    public static int snowTiresLevel = 0;
    public static int pizzaVelLevel = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        gasUpgradeImage.sprite = gasBarSprites[gasLevel];
        pizzaUpgradeImage.sprite = pizzaBarSprites[pizzaLevel];
        tireUpgradeImage.sprite = tireBarSprites[snowTiresLevel];
        pizzaVelUpgradeImage.sprite = pizzaVelBarSprites[pizzaVelLevel];
    }
    public void startRun()
    {
        SceneManager.LoadScene("FullMap");
    }
    public void upgradeGas()
    {
        if (gasLevel >= 5)
        {
            return;
        }
        CarController.maxGas += 10;
        gasLevel += 1;
        gasUpgradeImage.sprite = gasBarSprites[gasLevel];
    }

    public void upgradePizzaCap()
    {
        if(pizzaLevel >= 5)
        {
            return;
        }
        CarController.maxPizza += 3;
        pizzaLevel += 1;
        pizzaUpgradeImage.sprite = pizzaBarSprites[pizzaLevel];

    }

    public void buySnowTires()
    {
        if(snowTiresLevel >= 1)
        {
            return;
        }
        CarController.snowTires = true;
        snowTiresLevel += 1;
        tireUpgradeImage.sprite = tireBarSprites[snowTiresLevel];

    }

    public void pizzaVelUpgrade()
    {
        if(pizzaVelLevel >= 3)
        {
            return;
        }

        CarController.pizzaVelocityMax += 25;   
        pizzaVelLevel += 1;
         pizzaVelUpgradeImage.sprite = pizzaVelBarSprites[pizzaVelLevel];
    }


}
