using JetBrains.Annotations;
using UnityEngine;

public class zoneScript : MonoBehaviour
{

    public int minTip;
    public int maxTip;
    public int area;

    private UIController uiController;
    

    void Start()
    {
        uiController = FindFirstObjectByType<UIController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Pizza"))
        {
           GiveTip();
           AreaNotify();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
    
    void GiveTip()
    {
         int tip = Random.Range(minTip, maxTip + 1);
            CarController.money += tip;
            uiController.ShowTipPopup(tip);
            Debug.Log(CarController.money);
    }

    void AreaNotify()
    {
        ZoneManager manager = transform.parent.gameObject.GetComponent<ZoneManager>();
        manager.IncreaseAreaCount(area);
    }
}
