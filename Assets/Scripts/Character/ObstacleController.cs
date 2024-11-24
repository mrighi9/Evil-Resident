using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Vector3 lastSafePosition; // To store the last safe position
    public float moveBackSpeed = 10f; // Speed for moving back to the safe position
    private bool isColliding = false; // Track if the player is colliding

    void Start()
    {
        // Initialize the last safe position to the player's starting position
        lastSafePosition = transform.position;
    }

    void Update()
    {
        if (!isColliding)
        {
            // Only save the safe position if the player is not inside an obstacle
            if (!Physics.CheckSphere(transform.position, 0.1f, LayerMask.GetMask("Obstacle")))
            {
                lastSafePosition = transform.position;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isColliding = true; // Mark as colliding

            // Calculate the direction away from the collision point
            Vector3 collisionNormal = collision.contacts[0].normal; // Normal vector of the collision
            Vector3 pushDirection = collisionNormal.normalized; // Ensure it's normalized

            // Move the player outside the collider
            transform.position += pushDirection * 0.05f; // Adjust the push magnitude as needed
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Continue pushing the player away if they're still inside the collider
            Vector3 collisionNormal = collision.contacts[0].normal;
            Vector3 pushDirection = collisionNormal.normalized;

            transform.position += pushDirection * 0.1f; // Small adjustments to ensure the player is outside
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isColliding = false; // Reset collision state
        }
    }
}
