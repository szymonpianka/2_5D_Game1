using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool disabled = false; // TELEPORT
    public float maximumSpeed;
    public float rotationSpeed;

    public float jumpHeight; // jumpSpeed

    [SerializeField]
    private float gravityMultiplier;

    [SerializeField]
    private float jumpHorizontalSpeed;

    public float jumpButtonGracePeriod;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;

    [SerializeField]
    private List<DragObjectScript> dragObjectScripts; // Zmienione na listę

    [SerializeField]
    private Crouch2Script crouch2Script; // Dodaj referencję do skryptu Crouch2Script

    private float originalRotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;

        // Zapamiętaj oryginalną wartość
        originalRotationSpeed = rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disabled)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector3 movementDirection = new Vector3(horizontalInput, 0);
            float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                inputMagnitude /= 2;
            }
            animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);
            movementDirection.Normalize();

            float gravity = Physics.gravity.y * gravityMultiplier;

            if (isJumping && ySpeed > 0 && Input.GetButton("Jump") == false)
            {
                gravity *= 2;
            }
            ySpeed += gravity * Time.deltaTime;

            if (characterController.isGrounded)
            {
                lastGroundedTime = Time.time;
            }

            if (Input.GetButtonDown("Jump"))
            {
                jumpButtonPressedTime = Time.time;
            }

            // Sprawdzenie wartości isDragging dla wszystkich skryptów w liście
            bool isAnyDragging = false;
            foreach (var dragScript in dragObjectScripts)
            {
                if (dragScript != null && dragScript.isDragging)
                {
                    isAnyDragging = true;
                    break;
                }
            }

            if (isAnyDragging)
            {
                rotationSpeed = 0;
                animator.SetBool("IsDragging", true);
            }
            else
            {
                rotationSpeed = originalRotationSpeed;
                animator.SetBool("IsDragging", false);
            }

            if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
            {
                characterController.stepOffset = originalStepOffset;
                ySpeed = -0.5f;
                animator.SetBool("IsGrounded", true);
                isGrounded = true;
                animator.SetBool("IsJumping", false);
                isJumping = false;
                animator.SetBool("IsFalling", false);

                if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod && !isAnyDragging && crouch2Script != null && !crouch2Script.isCrouching)
                {
                    ySpeed = Mathf.Sqrt(jumpHeight * -3 * gravity);
                    animator.SetBool("IsJumping", true);
                    isJumping = true;
                    jumpButtonPressedTime = null;
                    lastGroundedTime = null;
                }
            }
            else
            {
                characterController.stepOffset = 0;
                animator.SetBool("IsGrounded", false);
                isGrounded = false;

                if ((isJumping && ySpeed < 0) || ySpeed < -2)
                {
                    animator.SetBool("IsFalling", true);
                }
            }

            if (movementDirection != Vector3.zero)
            {
                animator.SetBool("IsMoving", true);
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

            if (isGrounded == false)
            {
                Vector3 velocity = movementDirection * inputMagnitude * jumpHorizontalSpeed;
                velocity.y = ySpeed;

                characterController.Move(velocity * Time.deltaTime);
            }
        }
    }

    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            Vector3 velocity = animator.deltaPosition;
            velocity.y = ySpeed * Time.deltaTime;

            characterController.Move(velocity);
        }
    }
    
        

}
