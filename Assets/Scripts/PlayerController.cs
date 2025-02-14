using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    
    private CharacterController controller;
    
   

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveDirection += cameraTransform.forward;
        if (Input.GetKey(KeyCode.S)) moveDirection -= cameraTransform.forward;
        if (Input.GetKey(KeyCode.A)) moveDirection -= cameraTransform.right;
        if (Input.GetKey(KeyCode.D)) moveDirection += cameraTransform.right;

        moveDirection.y = 0; 
        controller.Move(moveSpeed * Time.deltaTime * moveDirection.normalized);
    }   


}

