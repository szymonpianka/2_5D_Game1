using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Transform characterController;
    bool inside = false;
    bool nearLadder = false;
    public float speedUpDown = 3.2f;
    public PlayerMovement playerController;
    public List<GameObject> collisionExitObjects;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = gameObject.GetComponent<PlayerMovement>();
        inside = false;
        nearLadder = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            nearLadder = true;
        }

        if (collisionExitObjects.Contains(col.gameObject))
        {
            if (inside)
            {
                inside = false;
                playerController.enabled = true;
                animator.SetBool("IsClimbingLadder", false);
                animator.speed = 1.0f; // Przywróć prędkość animacji na normalną
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            nearLadder = false;
        }
    }

    void Update()
    {
        if (nearLadder && Input.GetButtonDown("Interact"))
        {
            inside = !inside;
            playerController.enabled = !inside;

            animator.SetBool("IsClimbingLadder", inside);

            if (inside)
            {
                Vector3 newRotation = characterController.rotation.eulerAngles;
                newRotation.y = 0;
                characterController.rotation = Quaternion.Euler(newRotation);
            }
            else
            {
                // Przywróć prędkość animacji na normalną, gdy gracz przestaje się wspinać
                animator.speed = 1.0f;
            }
        }

        if (inside)
        {
            bool isMovingUp = Input.GetKey("w") || Input.GetAxis("Vertical") > 0.9f;
            bool isMovingDown = Input.GetKey("s") || Input.GetAxis("Vertical") < -0.9f;

            if (isMovingUp)
            {
                characterController.transform.position += Vector3.up / speedUpDown;
            }
            else if (isMovingDown)
            {
                characterController.transform.position += Vector3.down / speedUpDown;
            }

            // Zatrzymaj animację, gdy gracz przestaje się poruszać
            if (isMovingUp || isMovingDown)
            {
                animator.speed = 1.0f; // Normalna prędkość animacji
            }
            else
            {
                animator.speed = 0.0f; // Zatrzymaj animację
            }
        }
    }
}
