using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch2Script : MonoBehaviour
{
    private CharacterController controller;

    // Variables for crouching
    private float originalHeight;
    private Vector3 originalCenter;
    private float crouchHeight;
    private Vector3 crouchCenter;
    public bool isCrouching = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        // Store the original height and center of the controller
        originalHeight = controller.height;
        originalCenter = controller.center;

        // Define the height and center of the controller when crouching (half of the original height)
        crouchHeight = originalHeight / 2;
        crouchCenter = originalCenter - new Vector3(0, (originalHeight - crouchHeight) / 2, 0);
    }

    void Update()
    {
        // Check if the left Ctrl key is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("IsCrawling", true);
            Debug.Log("ISCrawling true");
            Crouch();
        }

        // Check if the left Ctrl key is released
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
             animator.SetBool("IsCrawling", false);
            StandUp();
        }
    }

    void Crouch()
    {
        if (!isCrouching)
        {
            // Reduce the controller height and adjust the center for crouching
            controller.height = crouchHeight;
            controller.center = crouchCenter;
            isCrouching = true;
        }
    }

    void StandUp()
    {
        if (isCrouching)
        {
            // Restore the original controller height and center
            controller.height = originalHeight;
            controller.center = originalCenter;
            isCrouching = false;
        }
    }

    
    

   
}
