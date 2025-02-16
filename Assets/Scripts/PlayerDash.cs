using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private bool canDash = true;
    public bool IsDashing { get; private set; } = false;

    private Transform cameraTransform;
    private CharacterController controller;
    private Vector3 preDashVelocity; 
    private Vector3 currentVelocity; 

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    public void HandleDash(Vector3 playerVelocity)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash(playerVelocity));
        }
    }

    public Vector3 GetVelocity()
    {
        return currentVelocity;
    }

    private IEnumerator Dash(Vector3 playerVelocity)
    {
        canDash = false;
        IsDashing = true;

        // save movement velocity before dashing
        preDashVelocity = new Vector3(playerVelocity.x, 0f, playerVelocity.z); 

        // dash relative to the camera not movement
        Vector3 dashDirection = cameraTransform.forward;
        dashDirection.y = 0f;
        dashDirection.Normalize();

        // dash for dash duration
        currentVelocity = dashDirection * dashSpeed;
        yield return new WaitForSeconds(dashDuration);

        // reset speed after dash
        currentVelocity = preDashVelocity;

        IsDashing = false;

        
        yield return new WaitForSeconds(dashCooldown);// cool down timer
        canDash = true;
    }
}
