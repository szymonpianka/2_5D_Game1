using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch2Script : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D col;

    // Variables for crouching
    private Vector2 originalOffset;
    private Vector2 originalSize;
    private Vector2 crouchOffset;
    private Vector2 crouchSize;
    private bool isCrouching = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();

        // Store the original size and offset of the collider
        originalOffset = col.offset;
        originalSize = col.size;
        
        // Define the size and offset of the collider when crouching (1/4 of the original height)
        crouchSize = new Vector2(col.size.x, col.size.y / 4);
        crouchOffset = new Vector2(col.offset.x, col.offset.y - (originalSize.y - crouchSize.y) / 2);
    }

    void Update()
    {
        // Check if the left Ctrl key is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
        
        // Check if the left Ctrl key is released
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StandUp();
        }
    }

    void Crouch()
    {
        if (!isCrouching)
        {
            // Reduce the collider size and adjust the offset for crouching
            col.size = crouchSize;
            col.offset = crouchOffset;
            isCrouching = true;
        }
    }

    void StandUp()
    {
        if (isCrouching)
        {
            // Restore the original collider size and offset
            col.size = originalSize;
            col.offset = originalOffset;
            isCrouching = false;
        }
    }
    


    

   
}
