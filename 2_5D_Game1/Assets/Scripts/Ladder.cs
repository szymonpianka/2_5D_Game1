using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Transform characterController;
    bool inside = false;
    bool nearLadder = false; // Nowa zmienna śledząca, czy gracz jest blisko drabiny
    public float speedUpDown = 3.2f;
    public PlayerMovement playerController;
    public List<GameObject> collisionExitObjects; // Lista obiektów do sprawdzenia kolizji
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
            nearLadder = true; // Gracz jest blisko drabiny
        }

        // Sprawdź, czy kolizja jest z obiektem z listy
        if (collisionExitObjects.Contains(col.gameObject))
        {
            // Zakończ wspinanie, gdy gracz wejdzie w kolizję z obiektem z listy
            if (inside)
            {
                inside = false;
                playerController.enabled = true;
                animator.SetBool("IsClimbingLadder", false); // Ustaw animację na false
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            nearLadder = false; // Gracz odszedł od drabiny
        }
    }

    void Update()
    {
        if (nearLadder && Input.GetButtonDown("Interact"))
        {
            // Gracz wciśnie "E" będąc blisko drabiny
            inside = !inside; // Przełącz stan wspinania
            playerController.enabled = !inside;
            animator.SetBool("IsClimbingLadder", inside); // Ustaw animację na podstawie inside

            if (inside)
            {
                // Ustaw rotację Y gracza na 0
                Vector3 newRotation = characterController.rotation.eulerAngles;
                newRotation.y = 0;
                characterController.rotation = Quaternion.Euler(newRotation);
            }
        }

        if (inside)
        {
            // Ruch w górę za pomocą klawisza W lub joysticka
            if (Input.GetKey("w") || Input.GetAxis("Vertical") > 0.9f)
            {
                characterController.transform.position += Vector3.up / speedUpDown;
            }

            // Ruch w dół za pomocą klawisza S lub joysticka
            if (Input.GetKey("s") || Input.GetAxis("Vertical") < -0.9f)
            {
                characterController.transform.position += Vector3.down / speedUpDown;
            }
        }
    }
}
