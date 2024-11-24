using System.Collections;
using UnityEngine;

public class P1_Door : MonoBehaviour
{
    [SerializeField] GameObject fadeOut;
    [SerializeField] bool canTransfer;
    [SerializeField] float doorAnimationDuration = 8f; // Time to play the door opening animation
    [SerializeField] int doorSceneIndex = 2; // Index for the door animation scene
    [SerializeField] int targetSceneIndex = 3; // Index for the target scene after the animation

    void Update()
    {
        if (canTransfer)
        {
            if (Input.GetKey(KeyCode.E))
            {
                canTransfer = false;
                this.GetComponent<BoxCollider>().enabled = false;
                StartTransition();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTransfer = true;
        }
        else
        {
            canTransfer = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canTransfer = false;
    }

    private void StartTransition()
    {
        fadeOut.SetActive(true);
        StartCoroutine(WaitForAnimationAndTransition());
    }

    private IEnumerator WaitForAnimationAndTransition()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            // Wait for the animation to finish
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null; // Wait for the animation to complete
            }
        }

        // Transition once the animation finishes
        SceneTransitionManager.Instance.TransitionToScene(doorSceneIndex, targetSceneIndex, doorAnimationDuration);
    }
}
