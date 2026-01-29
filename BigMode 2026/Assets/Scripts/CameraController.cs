using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CameraController : MonoBehaviour


{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float mouseX;
    public float mouseY;

    public Transform Body;
    public Transform Head;

    public int camSens = 500; 

    public float Angle;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * camSens * Time.deltaTime;
        Body.Rotate(Vector3.up, mouseX);

        mouseY = Input.GetAxis("Mouse Y") * camSens * Time.deltaTime;
        Angle -= mouseY;
        Angle = Mathf.Clamp(Angle, -30, 45);
        Head.localRotation = Quaternion.Euler(Angle, 0, 0);
    }
}
