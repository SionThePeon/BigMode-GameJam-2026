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

    private int[] gasUpgradeCosts = new int[] {1,2,3,4,5};
    private int[] capacUpgradeCosts = new int[] {1,2,3,4,5};
    private int[] snowTirePrice = new int[] {5, 0};
    private int[] velUpgradeCosts = new int[] {2, 4, 6};


    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI gasUpgradeText;
    public TextMeshProUGUI capUpgradeText;
    public TextMeshProUGUI stUpgradeText;
    public TextMeshProUGUI pizzaVelText;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        gasUpgradeImage.sprite = gasBarSprites[gasLevel];
        pizzaUpgradeImage.sprite = pizzaBarSprites[pizzaLevel];
        tireUpgradeImage.sprite = tireBarSprites[snowTiresLevel];
        pizzaVelUpgradeImage.sprite = pizzaVelBarSprites[pizzaVelLevel];

        gasUpgradeText.text = gasUpgradeCosts[gasLevel].ToString();
        capUpgradeText.text = capacUpgradeCosts[pizzaLevel].ToString();
        stUpgradeText.text = snowTirePrice[snowTiresLevel].ToString();
        pizzaVelText.text = velUpgradeCosts[pizzaVelLevel].ToString();


    }

    public void Update()
    {
        moneyText.text = CarController.money.ToString();
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
            CarController.maxGas += 10;
            gasUpgradeText.text = gasUpgradeCosts[gasLevel].ToString();
            gasLevel += 1;
            gasUpgradeImage.sprite = gasBarSprites[gasLevel];
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
            CarController.maxPizza += 3;
            capUpgradeText.text = capacUpgradeCosts[pizzaLevel].ToString();
            pizzaLevel += 1;
            pizzaUpgradeImage.sprite = pizzaBarSprites[pizzaLevel];
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
            stUpgradeText.text = snowTirePrice[snowTiresLevel].ToString();
            snowTiresLevel += 1;
            tireUpgradeImage.sprite = tireBarSprites[snowTiresLevel];
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
            CarController.pizzaVelocityMax += 25;   
            pizzaVelText.text = velUpgradeCosts[pizzaVelLevel].ToString();
            pizzaVelLevel += 1;
            pizzaVelUpgradeImage.sprite = pizzaVelBarSprites[pizzaVelLevel];
        }
    }


}
