using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Data.SqlTypes;
using UnityEditor;

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

    private int[] gasUpgradeCosts = new int[] {2,10,20,100,200};
    private int[] capacUpgradeCosts = new int[] {10,30,50,90,150};
    private int[] snowTirePrice = new int[] {500, 0};
    private int[] velUpgradeCosts = new int[] {20, 70, 150};

    
    private int[] gasUpgradeBonus = new int[] {30, 50, 50 , 60, 55};
    private int[] capacUpgradeBonus = new int[] {4, 8, 10, 10, 10};
    private int[] velUpgradeBonus = new int[] {15, 25, 25};


    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI gasUpgradeText;
    public TextMeshProUGUI capUpgradeText;
    public TextMeshProUGUI stUpgradeText;
    public TextMeshProUGUI pizzaVelText;


    [SerializeField] private upgradeSoundEffects upgradeSoundEffects;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        gasUpgradeImage.sprite = gasBarSprites[gasLevel];
        pizzaUpgradeImage.sprite = pizzaBarSprites[pizzaLevel];
        tireUpgradeImage.sprite = tireBarSprites[snowTiresLevel];
        pizzaVelUpgradeImage.sprite = pizzaVelBarSprites[pizzaVelLevel];

        gasUpgradeText.text = "Gas Capacity: $" + gasUpgradeCosts[gasLevel].ToString();
        capUpgradeText.text = "Pizza Cap: $" + capacUpgradeCosts[pizzaLevel].ToString();
        stUpgradeText.text = "Snow Tires: $" + snowTirePrice[snowTiresLevel].ToString();
        pizzaVelText.text = "Pizza Velocity: $" + velUpgradeCosts[pizzaVelLevel].ToString();


    }

    public void Update()
    {
        moneyText.text = "$" + CarController.money.ToString();
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

        int cost = gasUpgradeCosts[gasLevel];
        if(CarController.money >= cost)
        {
            CarController.money -= cost;
            CarController.maxGas += gasUpgradeBonus[gasLevel];
            gasLevel += 1;
            gasUpgradeText.text = "Gas Capacity: $" + gasUpgradeCosts[gasLevel].ToString();
            gasUpgradeImage.sprite = gasBarSprites[gasLevel];

            if(upgradeSoundEffects != null)
            {
                upgradeSoundEffects.PlayUpgradeSound();
            }

        }
    }

    public void upgradePizzaCap()
    {
        if(pizzaLevel >= 5)
        {
            return;
        }
        int cost = capacUpgradeCosts[pizzaLevel];
        if(CarController.money >= cost)
        {
            CarController.money -= cost;
            CarController.maxPizza += capacUpgradeBonus[pizzaLevel];
            pizzaLevel += 1;
            capUpgradeText.text = "Pizza Cap: $" + capacUpgradeCosts[pizzaLevel].ToString();
            pizzaUpgradeImage.sprite = pizzaBarSprites[pizzaLevel];

            if(upgradeSoundEffects != null)
            {
                upgradeSoundEffects.PlayUpgradeSound();
            }

        }
        

    }

    public void buySnowTires()
    {
        if(snowTiresLevel >= 1)
        {
            return;
        }
        int cost = snowTirePrice[snowTiresLevel];
        if(CarController.money >= cost)
        {
            CarController.money -= cost;
            CarController.snowTires = true;
            snowTiresLevel += 1;
            stUpgradeText.text = "Snow Tires: $" + snowTirePrice[snowTiresLevel].ToString();
            tireUpgradeImage.sprite = tireBarSprites[snowTiresLevel];

            if(upgradeSoundEffects != null)
            {
                upgradeSoundEffects.PlayUpgradeSound();
            }

        }

    }

    public void pizzaVelUpgrade()
    {
        if(pizzaVelLevel >= 3)
        {
            return;
        }
        int cost = velUpgradeCosts[pizzaVelLevel];
        if(CarController.money >= cost)
        {
            CarController.money -= cost;
            CarController.pizzaVelocityMax += velUpgradeBonus[pizzaLevel];   
            pizzaVelLevel += 1;
            pizzaVelText.text = "Pizza Velocity: $" + velUpgradeCosts[pizzaVelLevel].ToString();
            pizzaVelUpgradeImage.sprite = pizzaVelBarSprites[pizzaVelLevel];

            if(upgradeSoundEffects != null)
            {
                upgradeSoundEffects.PlayUpgradeSound();
            }

        }
    }


}
