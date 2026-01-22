using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    float xDir;
    float zDir;
    float yDir;
    float jumpforce = 10;
    bool jumpKey;
    public float playerSpeed;
    





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        xDir = Input.GetAxis("Horizontal");
        zDir = Input.GetAxis("Vertical");
        jumpKey = Input.GetKey(KeyCode.Space);

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(xDir, 0, zDir);

        moveDir.Normalize();
        if (jumpKey && IsGrounded() && yDir == 0)
        {
            yDir = jumpforce;
        }
        else
        {
            yDir = 0;
        }
        
    }


}
