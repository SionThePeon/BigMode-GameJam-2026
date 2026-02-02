using System;
using System.Data.Common;
using System.Threading;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEditor.SearchService;
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

    public int pizzaCount = 10;

    public float gas = 20f;
    public float maxGas;


    void Start()
    {
       maxGas = gas;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        
        
        if(rb.linearVelocity.magnitude < maxSpeed)
        {
             rb.AddForce(forwardForce);
        }

    }

    void ShootPizza()
    {
        GameObject pizzaInstance = Instantiate(pizza, transform.position + transform.forward * 6.3f,Quaternion.identity);
        Rigidbody pizzaRB = pizzaInstance.GetComponent<Rigidbody>();
        pizzaRB.linearVelocity = rb.linearVelocity + transform.forward * 20f;
        pizzaCount --;
        
    }

}