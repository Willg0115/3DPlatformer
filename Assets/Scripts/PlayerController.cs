using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -20f;//gravity, not using rigid body 
    public float fallMultiplier = 2f; // adjust gravity when falling
    public float jumpMultiplier = 1.5f; // adjust gravity while going up
    public Transform cameraTransform;

    [Header("Jump Settings")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.3f;
    public bool doubleJump = true;
    
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private int jumpCount = 0;

    
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }


    void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveDirection += cameraTransform.forward;
        if (Input.GetKey(KeyCode.S)) moveDirection -= cameraTransform.forward;
        if (Input.GetKey(KeyCode.A)) moveDirection -= cameraTransform.right;
        if (Input.GetKey(KeyCode.D)) moveDirection += cameraTransform.right;

        moveDirection.y = 0; 

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.15f);
        }
        controller.Move(moveSpeed * Time.deltaTime * moveDirection.normalized);

    }  

    void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            jumpCount = 0; 
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < (doubleJump ? 2 : 1))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpCount++;
        }
    }

    void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // keep player grounded
        }

        // down gravity multiplier
        if (velocity.y < 0)
        {
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * jumpMultiplier * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
 




