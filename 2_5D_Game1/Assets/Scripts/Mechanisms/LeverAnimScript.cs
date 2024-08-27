using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverAnimScript : MonoBehaviour
{
    // Animator for the lever
    private Animator animator;

    // Boolean variables to track lever state
    private bool isLeverOn = false;

    // Boolean to check if player is in the trigger zone
    private bool playerInRange = false;

    // Reference to the player object
    private GameObject player;

    // Public references to the GameObjects
    public GameObject OnObject;
    public GameObject OffObject;

    void Start()
    {
        // Get the Animator component attached to this game object
        animator = GetComponent<Animator>();

        // Ensure GameObjects are in the correct initial state
        UpdateObjects();
    }

    void Update()
    {
        // Check if the player is in range and if the 'E' key is pressed
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the lever state
            isLeverOn = !isLeverOn;

            // Set the appropriate animator boolean parameter
            if (isLeverOn)
            {
                animator.SetBool("IsLeverOn", true);
                animator.SetBool("IsLeverOff", false);
            }
            else
            {
                animator.SetBool("IsLeverOn", false);
                animator.SetBool("IsLeverOff", true);
            }
        }
    }

    // This function updates the GameObjects based on the lever's state
    public void UpdateObjects()
    {
        if (isLeverOn)
        {
            OnObject.SetActive(true);
            OffObject.SetActive(false);
        }
        else
        {
            OnObject.SetActive(false);
            OffObject.SetActive(true);
        }
    }

    // This function is called when another object enters a trigger collider attached to this object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
        }
    }

    // This function is called when another object leaves a trigger collider attached to this object
    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }
    
    
    
    
    
    
}
