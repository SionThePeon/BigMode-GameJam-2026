using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class menuController : MonoBehaviour
{
    public TextMeshProUGUI gasUpgrade;
    public TextMeshProUGUI pizzaUpgrade;
    public TextMeshProUGUI tireUpgrade;
    public TextMeshProUGUI pizzaVelUpgradeText;

    public int gasLevel = 0;
    public int pizzaLevel = 0;
    public int snowTiresLevel = 0;
    public int pizzaVelLevel = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        gasUpgrade.text = "Upgrade Level: " + gasLevel + " / 5";
        pizzaUpgrade.text = "Upgrade Level: " + pizzaLevel + " / 5";
        tireUpgrade.text = "Upgrade Level: " + snowTiresLevel + " / 1";
        pizzaVelUpgradeText.text = "Upgrade Level: " + pizzaVelLevel + " / 3";
    }
    public void startRun()
    {
        SceneManager.LoadSceneAsync("FullMap");
    }

    public void upgradeGas()
    {
        if (gasLevel >= 5)
        {
            return;
        }
        CarController.maxGas += 10;
        gasLevel += 1;
        gasUpgrade.text = "Upgrade Level: " + gasLevel + " / 5";
    }
    public void upgradePizzaCap()
    {
        if(pizzaLevel >= 5)
        {
            return;
        }
        CarController.maxPizza += 3;
        pizzaLevel += 1;
        pizzaUpgrade.text = "Upgrade Level: " + pizzaLevel + " / 5";

    }

    public void buySnowTires()
    {
        if(snowTiresLevel >= 1)
        {
            return;
        }
        CarController.snowTires = true;
        snowTiresLevel += 1;
        tireUpgrade.text = "Upgrade Level: " + snowTiresLevel + " / 1";

    }

    public void pizzaVelUpgrade()
    {
        if(pizzaVelLevel >= 3)
        {
            return;
        }

        CarController.pizzaVelocityMax += 25;   
        pizzaVelLevel += 1;
        pizzaVelUpgradeText.text = "Upgrade Level: " + pizzaVelLevel + " / 3";
    }


}
