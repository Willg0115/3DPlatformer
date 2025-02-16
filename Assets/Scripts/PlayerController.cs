using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -60f;//gravity, not using rigid body 
    public float fallMultiplier = 1.7f; // adjust gravity when falling
    public Transform cameraTransform;

    [Header("Jump Settings")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.3f;
    public bool doubleJump = true;

    [Header("Dash Settings")]
    public float dashSpeed = 500f;
    public float dashDuration = 1f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false; 
    
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
        HandleDash();
        // Only process movement if not dashing.
        if (!isDashing)
        {
            HandleMovement();
        }
        HandleJump();
        ApplyGravity();
    }

    private void HandleDash(){
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(dash());
        }
        IEnumerator dash()//
        {
            canDash = false;
            isDashing = true;

            // Save the current velocity componenents so gravity/jump and movespeed is preserved.
            float originalY = velocity.y;
            float originalX = velocity.x;
            float originalZ = velocity.z;

            Vector3 dashDirection = cameraTransform.forward; //dash forward relative to camera/mouse not movement direction
            dashDirection.y = 0f; 
            dashDirection.Normalize();

            //dash for dash duration
            velocity = dashDirection * dashSpeed;
            velocity.y = originalY;
            yield return new WaitForSeconds(dashDuration);

            isDashing = false;

            // Reset speed 
            velocity.x = originalX;
            velocity.z = originalZ;

            yield return new WaitForSeconds(dashCooldown);//cool down timer
            canDash = true;
        }
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
            velocity.y += gravity * fallMultiplier * Time.deltaTime;//going down
        }
        else
        {   
            velocity.y += gravity * Time.deltaTime;//going up
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
 




