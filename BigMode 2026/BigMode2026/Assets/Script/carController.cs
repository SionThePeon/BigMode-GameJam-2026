using System;
using System.Data.Common;
using System.Threading;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using Unity.VisualScripting;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    private float drain;
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
    public static float maxGas = 120f;

    public static int money = 50;
    private bool slow;

    public static bool snowTires = false;

    public static float pizzaVelocity;
    public static float pizzaVelocityMax = 20f;

    private bool bridge = false;

    public ParticleSystem smokeEffect;

    [SerializeField] private soundEffectManager soundManager;

    // private bool land = false;

    // private float zWaitTime = 1f;


    void Start()
    {
        drain = 1f;
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

        if (Input.GetKey(KeyCode.Space) && rb.linearVelocity.magnitude > 5f)
        {
        if (!smokeEffect.isPlaying)
            {
            smokeEffect.Play();
            soundManager.PlayDriftSound();
            }
        }
        else
        {
        if (smokeEffect.isPlaying)
            {
            smokeEffect.Stop();
            }
        }
    }

    void Timer()
    {
        gas -= Time.deltaTime * drain;
        if (gas <= 0f)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Mechanic");
        }
    }

    void ClampXRotation()
    {
        float maxAngle = 45f;
        float snap = 0.2f;
        Quaternion rot = rb.rotation;
        Vector3 euler = rot.eulerAngles;
        float x = euler.x > 180f ? euler.x - 360f : euler.x;
        float clamped = Mathf.Clamp(x, -maxAngle, maxAngle);
        if (!Mathf.Approximately(x, clamped))
        {
            euler.x = Mathf.Lerp(x, clamped, snap);
            rb.MoveRotation(Quaternion.Euler(euler));
        }
    }
    void ClampZRotation()
    {
        float snap = 0.2f;
        Quaternion rot = rb.rotation;
        Vector3 euler = rot.eulerAngles;
        float z = euler.z > 180f ? euler.z - 360f : euler.z;
        float clamped = Mathf.Clamp(z, -0.05f, 0.05f);
        if (!Mathf.Approximately(z, clamped))
        {
            euler.z = Mathf.Lerp(z, clamped, snap);
            rb.MoveRotation(Quaternion.Euler(euler));
        }
    }

    void FixedUpdate()
    {
        ClampXRotation();
        ClampZRotation();
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
        if (IsGrounded()){
        // if (land)
        //     {
        //         zWaitTime -= Time.deltaTime;
        //         rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
        //         if (zWaitTime < 0f)
        //         {
        //             rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
        //             land = false;
        //             zWaitTime = 1f;

        //         }
        //     }
        float limit = maxSpeed;
        if (slow)
        {
            limit = maxSpeed/10f;
        }
        else if (bridge)
        {
            forwardForce *= 2f;
            limit = maxSpeed * 4;
        }
        if(rb.linearVelocity.magnitude < limit)
        {
             rb.AddForce(forwardForce);
        }
        }
        // else
        // {
        //     land = true;
        // }
        if (rb.linearVelocity.y > 20f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 20f, rb.linearVelocity.z);
        }

    }

    void ShootPizza()
    {
        GameObject pizzaInstance = Instantiate(pizza, transform.position + transform.forward * 6.3f,Quaternion.identity);
        Rigidbody pizzaRB = pizzaInstance.GetComponent<Rigidbody>();
        //pizzaRB.linearVelocity = rb.linearVelocity + transform.forward * 20f;
        pizzaRB.linearVelocity = rb.linearVelocity + transform.forward * pizzaVelocity;
        pizzaCount --;
        soundManager.PlayPizzaShootSound();
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
        else if (other.tag == "Bridge")
        {
            drain = 2.5f;
            bridge = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Snow")
        {
            slow = false;
        }
        else if (other.tag == "Bridge")
        {
            bridge = false;
            drain = 1f;
        }
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(this.transform.position, Vector3.down, 0.6f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}