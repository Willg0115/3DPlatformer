using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.1f;
    public bool doubleJump = true;
    
    private int jumpCount = 0;
    private bool isGrounded;

    public void HandleJump(ref Vector3 velocity)
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < (doubleJump ? 2 : 1))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * PlayerGravity.gravity);
            jumpCount++;
        }
    }
}
