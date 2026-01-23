using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerActionMap playerControls;

    [SerializeField] private CharacterController controller;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;

    private Vector2 moveInput;
    private Vector3 velocity;
    private bool isGrounded;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControls = new PlayerActionMap();
    }


    void Update()
    {
       isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnJump()
    {
         if (!isGrounded) return;

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void OnMovement(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }
}
