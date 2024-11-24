using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    public GameObject thePlayer; // The player object with the Animator
    public float walkSpeed = 5f; // Speed for walking
    public float runSpeed = 10f; // Speed for running
    public float rotationSpeed = 100f; // Rotation speed
    private Animator playerAnimator; // Cached Animator component
    private bool isRunning; // Indicates if the player is running
    private float verticalInput; // Vertical input value
    private float horizontalInput; // Horizontal input value

    void Start()
    {
        // Cache the Animator component at the start
        playerAnimator = thePlayer.GetComponent<Animator>();
    }

    void Update()
    {
        if(WeaponMechanics.isAiming == false){
        // Get input values
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift) && verticalInput > 0; // Only allow running forward

        // Handle movement
        HandleMovement();

        // Handle animation states
        UpdateAnimations();
        }
    }

    private void HandleMovement()
    {
        // Handle rotation
        if (horizontalInput != 0)
        {
            float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
            thePlayer.transform.Rotate(0, rotationAmount, 0);
        }

        // Handle forward/backward movement
        if (verticalInput != 0)
        {
            float moveSpeed = (verticalInput > 0 && isRunning) ? runSpeed : walkSpeed; // Use walking speed if moving backward
            float moveAmount = verticalInput * moveSpeed * Time.deltaTime;
            thePlayer.transform.Translate(0, 0, moveAmount);
        }
    }

    private void UpdateAnimations()
    {
        // Determine animation based on movement and running state
        if (verticalInput > 0)
        {
            playerAnimator.Play(isRunning ? "Running" : "Walking");
        }
        else if (verticalInput < 0)
        {
            playerAnimator.Play("WalkingBackwards");
        }
        else if (horizontalInput != 0)
        {
            playerAnimator.Play("Idle");
        }
        else
        {
            playerAnimator.Play("Idle");
        }
    }
}
