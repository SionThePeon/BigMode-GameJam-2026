using UnityEngine;

public class endingScript : MonoBehaviour
{
    public ZoneManager manager;
    public GameObject finalZone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Start()
    {
        finalZone.SetActive(false);
    }
    
    void Update()
    {
        if (manager.area3Count == manager.area3Total)
        {
            finalZone.SetActive(true);
        }
        
    }
}
