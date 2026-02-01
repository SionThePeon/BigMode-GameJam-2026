using System.Data.Common;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 InputKey;

    public GameObject pizza;

    [SerializeField] private float carSpeed;
    [SerializeField] private float backSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float rotSpeed;

    [SerializeField] private float maxSpeed;


    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShootPizza();
        }
        InputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        int direction;
        float speedMultiplier;
        if (InputKey.z >= 0)
        {
            direction = 1;
            speedMultiplier = carSpeed;
        }
        else
        {   
            direction = -1;
            speedMultiplier = backSpeed;
        }
        Vector3 forwardForce = transform.forward * InputKey.z * speedMultiplier;
        Vector3 lateralForce = transform.right * InputKey.x * turnSpeed;
        if (Input.GetKey(KeyCode.Space))
        {
            rotSpeed = 140;
            rb.linearDamping = 0.45f;
        }
        else
        {
            rotSpeed = 80;
            rb.linearDamping = 0.05f;
        }
        float yRot = rotSpeed * Time.fixedDeltaTime * InputKey.x * direction;
        Quaternion deltaRotation = Quaternion.Euler(0, yRot, 0);

        rb.MoveRotation(rb.rotation * deltaRotation );
        if(math.abs(rb.linearVelocity.magnitude) < maxSpeed)
        {
             rb.AddForce(forwardForce + lateralForce);
        }

    }

    void ShootPizza()
    {
        GameObject pizzaInstance = Instantiate(pizza, transform.position + transform.forward * 6.3f,Quaternion.identity);
        Rigidbody pizzaRB = pizzaInstance.GetComponent<Rigidbody>();
        pizzaRB.linearVelocity = rb.linearVelocity + transform.forward * 20f;
        
    }

}