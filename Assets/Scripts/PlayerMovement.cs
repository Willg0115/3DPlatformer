using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f;
    public Transform cameraTransform;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void HandleMovement()
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
}
