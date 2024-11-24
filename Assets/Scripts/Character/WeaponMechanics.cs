using System.Collections;
using UnityEngine;

public class WeaponMechanics : MonoBehaviour
{
    public static bool isAiming = false;
    public GameObject thePlayer;
    public float horizontalMove;
    public AudioSource pistolShot;
    public bool isFiring = false;
    public float fireRate = 1f; // Adjust as needed
    public bool canFire = false;
    private bool queuedReturn = false; // Track if ReturnToTank is queued

    void Update()
    {
        // Start aiming
        if (Input.GetMouseButtonDown(1) && !isAiming)
        {
            PlayAnimation("IdleToAim");
            canFire = false;
            StartCoroutine(WaitForBlendTree("IdleToAim"));
            
            canFire = true;
            isAiming = true;
        }

        // Queue ReturnToTank if aiming button is released
        if (Input.GetMouseButtonUp(1) && isAiming && canFire && !isFiring)
        {
            canFire = false;
            StartCoroutine(ReturnToTank());
        }
        else if (Input.GetMouseButtonUp(1) && isFiring)
        {
            queuedReturn = true; // Queue ReturnToTank to execute after firing
        }

        // Handle aiming controls
        if (isAiming)
        {
            if (Input.GetButton("Horizontal"))
            {
                horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
                thePlayer.transform.Rotate(0, horizontalMove, 0);
            }

            // Handle firing
            if (Input.GetMouseButtonDown(0) && !isFiring && canFire)
            {
                isFiring = true;
                StartCoroutine(FiringWeapon());
            }
        }
    }

    IEnumerator FiringWeapon()
    {
        PlayAnimation("PistolShoot");
        pistolShot.Play();

        // Wait for the firing animation to complete
        yield return new WaitForSeconds(fireRate);

        PlayAnimation("PistolAim");
        

        isFiring = false;

        // If ReturnToTank is queued, execute it now
        if (queuedReturn)
        {
            queuedReturn = false;
            StartCoroutine(ReturnToTank());
        }
    }

    IEnumerator ReturnToTank()
    {
        PlayAnimation("AimToIdle");

        // Wait for the AimToIdle animation to complete
        yield return new WaitForSeconds(0.25f);

        isAiming = false;
    }

    IEnumerator WaitForBlendTree(string stateName)
    {
        Animator animator = thePlayer?.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on thePlayer object.");
            yield break;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Wait until the Blend Tree state is active
        while (!stateInfo.IsName(stateName))
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        Debug.Log($"Blend Tree '{stateName}' is now active.");
    }

    private void PlayAnimation(string animationName)
    {
        Animator animator = thePlayer?.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play(animationName);
        }
        else
        {
            Debug.LogError("Animator component not found on thePlayer object.");
        }
    }
}
