using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectScript : MonoBehaviour
{
    private Transform playerTransform;
    public Transform newParentTransform; // Przypisz ten obiekt w inspektorze
    public bool isDragging = false;
    private bool isPlayerInTrigger = false;
    private Crouch2Script crouchScript; // Referencja do skryptu Crouch2Script

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        crouchScript = playerTransform.GetComponent<Crouch2Script>(); // Pobierz skrypt Crouch2Script z gracza
    }

    void Update()
    {
        if (isPlayerInTrigger)
        {
            // Sprawdź, czy gracz nie jest w pozycji kucającej
            if (Input.GetKeyDown(KeyCode.E) && !isDragging && crouchScript != null && !crouchScript.isCrouching)
            {
                Debug.Log("Rozpoczęcie przeciągania obiektu");
                transform.SetParent(playerTransform);
                isDragging = true;
            }

            if (Input.GetKeyUp(KeyCode.E) && isDragging)
            {
                Debug.Log("E zwolniony");
                transform.SetParent(newParentTransform); // Przypisanie nowego rodzica
                isDragging = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Gracz wszedł w trigger");
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Gracz opuścił trigger");
            isPlayerInTrigger = false;
            if (isDragging)
            {
                Debug.Log("Resetowanie stanu przeciągania");
                transform.SetParent(newParentTransform);
                isDragging = false;
            }
        }
    }
    
    
}
