using System.Data.Common;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 InputKey;
    [SerializeField] private float carSpeed;
    [SerializeField] private float backSpeed;
    [SerializeField] private float turnSpeed;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        InputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        float speedMultiplier;
        if (InputKey.z >= 0)
        {
            speedMultiplier = carSpeed;
        }
        else
        {   
            speedMultiplier = backSpeed;
        }
        Vector3 forwardForce = transform.forward * InputKey.z * speedMultiplier;
        Vector3 lateralForce = transform.right * InputKey.x * turnSpeed;

        rb.AddForce(forwardForce + lateralForce);

        float rotSpeed = 30;
        float yRot = rotSpeed * Time.fixedDeltaTime * InputKey.x;
        Quaternion deltaRotation = Quaternion.Euler(0, yRot, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);

    }

}