using System;
using System.Data.Common;
using System.Threading;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 InputKey;

    public GameObject pizza;

    [SerializeField] private float carSpeed;
    [SerializeField] private float backSpeed; 
    private float rotSpeed;

    [SerializeField] private float maxSpeed;

    public static int pizzaCount;
    public static int maxPizza = 10;

    public static float gas;
    public static float maxGas = 40f;

    public static int money = 0;
    private bool slow;

    public static bool snowTires = false;

    public static float pizzaVelocity;
    public static float pizzaVelocityMax = 20f;

    void Start()
    {
        slow = false;
        gas = maxGas;
        pizzaCount = maxPizza;
        pizzaVelocity = pizzaVelocityMax;
       rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && pizzaCount > 0)
        {
            ShootPizza();
        }
        InputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Timer();
    }

    void Timer()
    {
        gas -= Time.deltaTime;
        if (gas <= 0f)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Mechanic");
        }
    }

    void FixedUpdate()
    {
        int direction;
        float speedMultiplier;
        if (InputKey.z >= 0)
        {
            speedMultiplier = carSpeed;
        }
        else
        {  
            speedMultiplier = backSpeed;
        }
        float dot = Vector3.Dot(rb.linearVelocity, transform.forward);
        if (dot >= 0f)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        Vector3 forwardForce = transform.forward * InputKey.z * speedMultiplier;
        if (Input.GetKey(KeyCode.Space))
        {
            rotSpeed = 160;
            rb.linearDamping = 0.45f;
        }
        else
        {
            rotSpeed = 100;
            rb.linearDamping = 0.1f;
        }
        float turningLock = rb.linearVelocity.magnitude * 0.4f;
        if (turningLock > 1f)
        {
            turningLock = 1f;
        }
        float yRot = rotSpeed * Time.fixedDeltaTime * InputKey.x * direction * turningLock;
        Quaternion deltaRotation = Quaternion.Euler(0, yRot, 0);
        
        
        rb.MoveRotation(rb.rotation * deltaRotation);
        
        float limit = maxSpeed;
        if (slow)
        {
            limit = maxSpeed/10f;
        }
        if(rb.linearVelocity.magnitude < limit)
        {
             rb.AddForce(forwardForce);
        }

    }

    void ShootPizza()
    {
        GameObject pizzaInstance = Instantiate(pizza, transform.position + transform.forward * 6.3f,Quaternion.identity);
        Rigidbody pizzaRB = pizzaInstance.GetComponent<Rigidbody>();
        //pizzaRB.linearVelocity = rb.linearVelocity + transform.forward * 20f;
        pizzaRB.linearVelocity = rb.linearVelocity + transform.forward * pizzaVelocity;
        pizzaCount --;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snow" && !snowTires)
        {   
            slow = true;
            if (rb.linearVelocity.magnitude > maxSpeed / 3)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * (maxSpeed/3);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Snow")
        {
            slow = false;
        }
    }

}