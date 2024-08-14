using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampRotationScript : MonoBehaviour
{
    public GameObject objectToRotate; // Obiekt, który ma się obracać
    public float rotationAngle = 90f; // Kąt obrotu w stopniach
    public float rotationTime = 2f; // Czas trwania obrotu
    private bool isPlayerInRange = false; // Czy gracz jest w zasięgu
    private bool isRotated = false; // Czy obiekt jest obrócony
    private bool isRotating = false; // Czy obiekt aktualnie się obraca
    private Coroutine currentRotationCoroutine = null; // Referencja do aktualnie działającej korutyny

    // Globalne wartości rotacji
    private Quaternion startRotation; // Początkowa rotacja
    private Quaternion targetRotation; // Docelowa rotacja

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            if (isRotated)
            {
                currentRotationCoroutine = StartCoroutine(RotateObject(-rotationAngle)); // Obróć w przeciwną stronę
            }
            else
            {
                currentRotationCoroutine = StartCoroutine(RotateObject(rotationAngle)); // Obróć o zadany kąt
            }
            isRotated = !isRotated; // Zmień stan obrotu
        }
    }

    private IEnumerator RotateObject(float angle)
    {
        isRotating = true; // Ustaw flagę, że obiekt się obraca
        startRotation = objectToRotate.transform.rotation;

        // Ustaw docelową rotację na podstawie aktualnej rotacji
        targetRotation = startRotation * Quaternion.Euler(0, 0, angle);

        float elapsedTime = 0f;

        while (elapsedTime < rotationTime)
        {
            if (!isRotating)
            {
                yield break; // Jeśli rotacja została zatrzymana, zakończ korutynę
            }

            float t = elapsedTime / rotationTime;
            objectToRotate.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ustaw rotację na końcowy stan
        objectToRotate.transform.rotation = targetRotation;
        isRotating = false; // Zresetuj flagę po zakończeniu obrotu
        currentRotationCoroutine = null;
    }

    public void StopRotation()
    {
        if (currentRotationCoroutine != null)
        {
            StopCoroutine(currentRotationCoroutine); // Zatrzymaj aktualną korutynę
            isRotating = false; // Ustaw flagę, że obiekt przestał się obracać
            currentRotationCoroutine = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
    
    
    
}
