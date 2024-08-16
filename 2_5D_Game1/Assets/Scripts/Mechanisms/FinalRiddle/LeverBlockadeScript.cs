using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBlockadeScript : MonoBehaviour
{
    public PowerSwitchFRScript powerSwitchFR; // Referencja do obiektu PowerSwitchFR
    public Transform objectToRotate; // Obiekt, który chcemy obrócić
    public float rotationAmount = 90f; // Ilość stopni, o które ma się obrócić obiekt
    public float rotationSpeed = 45f; // Prędkość obrotu w stopniach na sekundę

    private bool isPlayerInTrigger = false;
    private bool hasActivated = false;
    private float currentRotation = 0f; // Śledzi aktualną ilość obrotu

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetButtonDown("Interact") && !hasActivated)
        {
            if (powerSwitchFR != null && powerSwitchFR.PowerFROn)
            {
                hasActivated = true; // Ustawia, że akcja została rozpoczęta
            }
        }

        // Jeśli akcja została rozpoczęta i obiekt nie osiągnął jeszcze pełnej rotacji
        if (hasActivated && currentRotation < rotationAmount)
        {
            // Oblicza ile stopni obrócić obiekt w tej klatce
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            float rotationToApply = Mathf.Min(rotationThisFrame, rotationAmount - currentRotation);

            // Obraca obiekt
            objectToRotate.Rotate(0, 0, rotationToApply);

            // Aktualizuje aktualną ilość obrotu
            currentRotation += rotationToApply;
        }
    }
    
    
    
    
}
