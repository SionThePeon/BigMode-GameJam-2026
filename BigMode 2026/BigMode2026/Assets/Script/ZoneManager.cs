using System;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public GameObject car;
    private List<GameObject> zones = new();

    public GameObject arrow;

    private GameObject arrowInstance;

    private int area1Count = 0;
    private int area2Count = 0;
    private int area3Count = 0;

    public int area1Total = 4;
    public int area2Total = 5;

    public int area3Total = 5;

    public int area1Bonus = 100;
    public int area2Bonus = 200;
    public int area3Bonus = 300;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        foreach (Transform child in transform)
        {
            zones.Add(child.gameObject);
        }
        arrowInstance = Instantiate(arrow, car.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        zones.RemoveAll(t => t == null);
        if (zones.Count != 0)
        {
            GameObject nearest = GetNearest();
            Vector3 dir = car.transform.position - nearest.transform.position;
            Vector3 dirLocked = new Vector3(dir.x, 0f, dir.z).normalized;
            arrowInstance.transform.rotation = Quaternion.LookRotation(dirLocked);
            arrowInstance.transform.position = car.transform.position - dirLocked * 10f;
        }
        else if (arrowInstance != null)
        {
            Destroy(arrowInstance);
        }
    }
    GameObject GetNearest()
    {
        GameObject nearest = zones[0];
        float minDist = Math.Abs((car.transform.position - nearest.transform.position).magnitude);
        foreach (GameObject zone in zones)
        {
            float dist = Math.Abs((car.transform.position - zone.transform.position).magnitude);
            if (dist < minDist)
            {
                nearest = zone;
                minDist = dist;
            }
        }
        return nearest;
    }

    public void IncreaseAreaCount(int code)
    {
        if (code == 1)
        {
            area1Count ++;
            if (area1Count == area1Total)
            {
                CarController.money += area1Bonus;
            }
        }
        else if (code == 2)
        {
            area2Count ++;
            if (area2Count == area2Total)
            {
                CarController.money += area2Bonus;
            }
        }
        else if (code == 3)
        {
            area3Count ++;
            if (area3Count == area3Total)
            {
                CarController.money += area3Bonus;
            }
        }
    }

    }
    
