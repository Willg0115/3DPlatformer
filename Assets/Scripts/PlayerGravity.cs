using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public static float gravity = -60f;
    public float fallMultiplier = 1.7f;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void ApplyGravity(ref Vector3 velocity)
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //down gravity multiplier for less floaty
        if (velocity.y < 0)
        {
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }
}
