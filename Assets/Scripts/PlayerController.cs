using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerMovement movement;
    private PlayerJump jump;
    private PlayerDash dash;
    private PlayerGravity gravity;

    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        dash = GetComponent<PlayerDash>();
        gravity = GetComponent<PlayerGravity>();
    }

    void Update()
    {
        dash.HandleDash(velocity);
        //only handle movemnt if not dashing
        if (!dash.IsDashing)
        {
            movement.HandleMovement();
        }

        jump.HandleJump(ref velocity);
        gravity.ApplyGravity(ref velocity);

        // get velocity from dash (if dashing, uses dash velocity, otherwise normal velocity)
        velocity.x = dash.GetVelocity().x;
        velocity.z = dash.GetVelocity().z;

        controller.Move(velocity * Time.deltaTime);
    }

}


