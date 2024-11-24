using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionRadius = 15f; // How far the zombie can detect the player
    public float fieldOfViewAngle = 90f; // Field of view in degrees

    [Header("Chase Settings")]
    public float chaseSpeed = 3.5f;
    public float patrolSpeed = 1.5f;

    [Header("References")]
    public Transform player; // Assign the player transform in the Inspector
    public NavMeshAgent agent; // Assign the NavMeshAgent component

    private bool isAggro = false;
    private Animator animator;

    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAggro)
        {
            Patrol(); // Implement patrol behavior if desired
            DetectPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    void Patrol()
    {
        // Optional: Implement patrol points or idle behavior
        // For now, zombies remain idle when not aggroed
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if within detection radius
        if (distanceToPlayer <= detectionRadius)
        {
            // Check if within FOV
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= fieldOfViewAngle / 2f)
            {
                // Raycast to check line of sight
                RaycastHit hit;
                Vector3 rayOrigin = transform.position + Vector3.up * 1.5f; // Adjust height as needed
                if (Physics.Raycast(rayOrigin, directionToPlayer, out hit, detectionRadius))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        isAggro = true;
                        agent.speed = chaseSpeed;
                        Debug.Log($"{gameObject.name} spotted the player and is now aggroed!");

                        // Optionally, trigger animation state
                        if (animator != null)
                            animator.SetBool("isChasing", true);
                    }
                }
            }
        }
    }

    void ChasePlayer()
    {
        if (agent != null)
        {
            agent.SetDestination(player.position);
        }

        // Optionally, stop chasing if player escapes detection radius
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRadius * 1.5f) // Add buffer
        {
            isAggro = false;
            agent.ResetPath();
            agent.speed = patrolSpeed;
            Debug.Log($"{gameObject.name} lost sight of the player.");

            if (animator != null)
                animator.SetBool("isChasing", false);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize detection radius and FOV in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle / 2f, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle / 2f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftBoundary * detectionRadius);
        Gizmos.DrawRay(transform.position, rightBoundary * detectionRadius);
    }

    // Optional: Reset aggro when zombie dies or other conditions
    public void ResetAggro()
    {
        isAggro = false;
        if (agent != null)
            agent.ResetPath();
        if (animator != null)
            animator.SetBool("isChasing", false);
    }
}
